using System;
using RPG.FSM;

namespace RPG.GameElements
{
    public interface IMp
    {
        float mp { get; set; }
        float mpMax { get; }
        float mpMin { get; }

        event Action<float> mnHpChanged;    //바뀐양 값
        event Action<float> mnHpRecovered;  //회복된 값
        event Action<float> mnHpDepleted;   //감소된 양
        event Action mnHpMax;
        event Action mnHpMin;
        
        
        void RecoverMP(MachineManager characterMachine, float amount);
        void DepleteMP(MachineManager characterMachine, float amount);
    }
}