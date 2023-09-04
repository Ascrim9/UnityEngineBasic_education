using System;
using RPG.Controllers;
using RPG.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _02.Scripts.Collections;
using RPG.GameElements;
using RPG.GameElements.Stats;
using UnityEngine;

namespace RPG.FSM
{
    public enum StateType
    {
        Idle,
        Move,
        Jump,
        Fall,
        Land,
        Somersault,
        Attack = 20,
    }


    [RequireComponent(typeof(Animator))]
    public class MachineManager : MonoBehaviour, IHp, IMp
    {
        public bool isGrounded => Physics.Raycast(transform.position + Vector3.up,
                                                  Vector3.down,
                                                  out RaycastHit hit,
                                                  _groundCastMaxDistance + 1.0f,
                                                  _groundMask);


        public UDictionary<StateType, Stat> stats;

        public StateType State;
        public Dictionary<int, Skill> skills;
        public Dictionary<int, float> skillCoolTimers;
        public Dictionary<int, bool> skillCastingDoneFlags;
        public bool hasJumped;
        public bool hasSomersaulted;

        private Animator _animator;

        private int _stateAnimHashID;
        private int _isDirty0AnimHashID;

        public Vector3 move;
        public float moveGain;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _groundCastMaxDistance;

        private Vector3 _inertia;
        private Rigidbody _rigidbody;

        public float horizontal;
        public float vertical;

        public Action onUpdate;
        private int _isDirty1AnimHashID;
        private static readonly int Vertical = Animator.StringToHash("vertical");
        private static readonly int Horizontal = Animator.StringToHash("horizontal");

        // ReSharper disable Unity.PerformanceAnalysis
        public bool UseSkill(int skillID)
        {
            if (SkillDataRepository.instance.activeSkillDatum.TryGetValue(skillID, out ActiveSkillData data) == false)
                return false;

            if (skillCoolTimers[skillID] > 0.0f)
            {
                // 콤보 가능 구간
                // if ((skillCastingDoneFlags[skillID] && skills[skillID].comboStack < data.comboStackMax))
                // {
                //     if (ChangeState(data.state))
                //     {
                //         skillCastingDoneFlags[skillID] = false;
                //         _animator.SetBool(_isDirty1AnimHashID, true);
                //         return true;
                //     }
                // }
                // return false;
                
                
                if ((!skillCastingDoneFlags[skillID] || skills[skillID].comboStack >= data.comboStackMax)) return false;
                if (!ChangeState(data.state)) return false;
                skillCastingDoneFlags[skillID] = false;
                _animator.SetBool(_isDirty1AnimHashID, true);
                return true;
            }

            if (!ChangeState(data.state)) return false;
            skillCoolTimers[skillID] = data.coolTime;
            skillCastingDoneFlags[skillID] = false;
            return true;

        }

        public bool ChangeState(Animator animator, StateType newState)
        {
            if (animator.GetInteger(_stateAnimHashID) == (int)newState)
                return false;

            animator.SetInteger(_stateAnimHashID, (int)newState);
            animator.SetBool(_isDirty0AnimHashID, true);
            Debug.Log($"[MachineManager] : Changed state to {newState}");
            return true;
        }

        public bool ChangeState(StateType newState)
        {
            if (_animator.GetInteger(_stateAnimHashID) == (int)newState)
                return false;

            _animator.SetInteger(_stateAnimHashID, (int)newState);
            _animator.SetBool(_isDirty0AnimHashID, true);
            Debug.Log($"[MachineManager] : Changed state to {newState}");
            return true;
        }

        protected virtual void Awake()
        {
            _stateAnimHashID = Animator.StringToHash("state");
            _isDirty0AnimHashID = Animator.StringToHash("isDirty0");
            _isDirty1AnimHashID = Animator.StringToHash("isDirty1");
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            BehaviourBase[] behaviours = _animator.GetBehaviours<BehaviourBase>();
            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].Initialize(this);
            }

            Skill[] skillArray = _animator.GetBehaviours<Skill>();

            skills = new Dictionary<int, Skill>();
            skillCoolTimers = new Dictionary<int, float>();
            skillCastingDoneFlags = new Dictionary<int, bool>();

            for (int i = 0; i < skillArray.Length; i++)
            {
                skills.Add(skillArray[i].skillID.value, skillArray[i]);
                skillCoolTimers.Add(skillArray[i].skillID.value, 0.0f);
                skillCastingDoneFlags.Add(skillArray[i].skillID.value, false);
            }

        }

        protected virtual void Update()
        {
            foreach (int skillID in skillCoolTimers.Keys.ToList())
            {
                if (skillCoolTimers[skillID] > 0.0f)
                {
                    skillCoolTimers[skillID] -= Time.deltaTime;
                    if (skillCoolTimers[skillID] <= 0.0f)
                    {
                        skills[skillID].comboStack = 0;
                        skillCoolTimers[skillID] = 0.0f;
                        skillCastingDoneFlags[skillID] = false;
                    }
                }
            }

            move = Quaternion.LookRotation(transform.forward, transform.up) * new Vector3(horizontal, 0.0f, vertical).normalized;
            _animator.SetFloat(Horizontal, Vector3.Dot(move * moveGain, transform.right));
            _animator.SetFloat(Vertical, Vector3.Dot(move * moveGain, transform.forward));

            onUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            if (isGrounded)
            {
                _inertia = move * moveGain;
                transform.position += move * (moveGain * Time.fixedDeltaTime);
            }
            else
            {
                transform.position += _inertia * Time.fixedDeltaTime;
                _inertia = Vector3.Lerp(_inertia, Vector3.zero, _rigidbody.drag);
            }
        }


        #region Animation event binded

        private void FootL() { }
        private void FootR() { }
        private void Land() { }

        private void Hit() { }
        #endregion


        private float _hp;

        public float hp
        {
            get => _hp;
            set
            {
                if (_hp.Equals(value))
                    return;
                var prev = _hp;
                _hp = value;

                onHpChanged?.Invoke((_hp));
                if (_hp > prev)
                {
                    onHpRecovered?.Invoke(value - prev);
                    if (!(_hp >= hpMax)) return;
                    _hp = hpMax;
                    onHpMax?.Invoke();
                }
                else
                {
                    onHpDepleted?.Invoke(prev - _hp);
                    if (!(_hp <= hpMin)) return;
                    _hp = hpMin;
                    onHpMin?.Invoke();
                }
            }
        }

        public float hpMax => stats[StateType.Somersault].valueModified;
        public float hpMin { get; }
        public event Action<float> onHpChanged;
        public event Action<float> onHpRecovered;
        public event Action<float> onHpDepleted;
        public event Action onHpMax;
        public event Action onHpMin;
        public void RecoverHP(MachineManager characterMachine, float amount)
        {
            throw new NotImplementedException();
        }

        public void DepleteHP(MachineManager characterMachine, float amount)
        {
            throw new NotImplementedException();
        }

        public float mp { get; set; }
        public float mpMax { get; }
        public float mpMin { get; }
        public event Action<float> mnHpChanged;
        public event Action<float> mnHpRecovered;
        public event Action<float> mnHpDepleted;
        public event Action mnHpMax;
        public event Action mnHpMin;
        public void RecoverMP(MachineManager characterMachine, float amount)
        {
            hp += amount;
        }

        public void DepleteMP(MachineManager characterMachine, float amount)
        {
            hp -= amount;
        }         
                  
        //TODO MP 
                  
        private void Reset()
        {
            Stats = new();
                 var statTypes = Enum.GetValues(typeof(StatType))
                     .Cast<StatType>()
                     .Where(x => x != StatType.None)
                     .Select(x => new UKeyValuePair<StatType, Stat>(x, new Stat(x, 0.0f)));
        }
    }
}