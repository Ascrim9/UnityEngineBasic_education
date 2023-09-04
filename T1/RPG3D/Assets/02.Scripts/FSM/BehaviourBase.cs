using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.FSM
{
    public class BehaviourBase : StateMachineBehaviour
    {
        protected MachineManager manager;
        protected Transform transform;
        protected Rigidbody rigidbody;
        private static readonly int IsDirty0 = Animator.StringToHash("isDirty0");
        private static readonly int IsDirty1 = Animator.StringToHash("isDirty1");

        public virtual void Initialize(MachineManager manager)
        {
            this.manager = manager;
            transform = manager.transform;
            rigidbody = manager.GetComponent<Rigidbody>();
        }

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);


        }
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            animator.SetBool(layerIndex == 0 ? IsDirty0 : IsDirty1, false);
            manager.onUpdate = () => OnUpdate(animator, layerIndex);
            
        }
        
        
        public virtual void OnUpdate(Animator animator, int layerIndex)
        {
                
        }
    }
}