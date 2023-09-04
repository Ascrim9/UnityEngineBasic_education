using System;
using RPG.FSM;

namespace RPG.GameElements
{
    public interface IHp
    {
        float hp { get; set; }
        float hpMax { get; }
        float hpMin { get; }

        event Action<float> onHpChanged;    //�ٲ�� ��
        event Action<float> onHpRecovered;  //ȸ���� ��
        event Action<float> onHpDepleted;   //���ҵ� ��
        event Action onHpMax;
        event Action onHpMin;
        
        
        void RecoverHP(MachineManager characterMachine, float amount);
        void DepleteHP(MachineManager characterMachine, float amount);

        
    }
}