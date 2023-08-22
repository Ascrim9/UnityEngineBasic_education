using RPG.GameElements.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//MonoBehaviour 를 굳이? 그냥 스텟조정자 어떻게 가지고 있는지에 대한 데이터만 SriotableObject 같은곳에 리스트로 넣어두로 런타임에 StatModifier 생성해도 됨.


public enum StatModifiyingOption
{
    None,
    AddFlat,
    AddPercent,
    MulPercent
}


public class StatModifier : MonoBehaviour
{
    public StatType type;
    public float value;

    

}
