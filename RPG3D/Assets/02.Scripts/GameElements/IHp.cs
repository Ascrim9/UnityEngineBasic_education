using System;
using RPG.FSM;

namespace RPG.GameElements
{
    public interface IHp
    {
        float hp { get; set; }
        float hpMax { get; }
        float hpMin { get; }

        event Action<float> onHpChanged;    //바뀐양 값
        event Action<float> onHpRecovered;  //회복된 값
        event Action<float> onHpDepleted;   //감소된 양
        event Action onHpMax;
        event Action onHpMin;
        
        
        void RecoverHP(MachineManager characterMachine, float amount);
        void DepleteHP(MachineManager characterMachine, float amount);

        
    }
}