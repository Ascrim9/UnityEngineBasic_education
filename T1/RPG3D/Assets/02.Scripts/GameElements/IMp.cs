using System;
using RPG.FSM;

namespace RPG.GameElements
{
    public interface IMp
    {
        float mp { get; set; }
        float mpMax { get; }
        float mpMin { get; }

        event Action<float> mnHpChanged;    //�ٲ�� ��
        event Action<float> mnHpRecovered;  //ȸ���� ��
        event Action<float> mnHpDepleted;   //���ҵ� ��
        event Action mnHpMax;
        event Action mnHpMin;
        
        
        void RecoverMP(MachineManager characterMachine, float amount);
        void DepleteMP(MachineManager characterMachine, float amount);
    }
}