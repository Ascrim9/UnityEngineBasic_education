
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Data;
using UnityEngine;

namespace RPG.GameElements
{

    public enum BodyPart
    {
        None,
        Head,
        Top,
        Bottom,
        RightFoot,
        LeftFoot,
        RightHand,
        LeftHand,
        TwoHand
    }


    public class Body : MonoBehaviour
    {
        public List<UKeyValuePair<BodyPart, Transform>> bodyParts;

        private void Start()
        {
            if (DataModelManager.instance.TryGet(out ItemsEquippendData itemsEquippendData))
            {
                itemsEquippendData.slotDatum.onItemChanged += (slotIndex, slotData)
                    =>
                {
                    //기존 파괴
                    if (bodyParts[slotIndex].Value.childCount is 1)
                    {
                        Destroy(bodyParts[slotIndex].Value.GetChild(0).gameObject);
                    }


                    //새로 생성 함
                    if (slotData.isEmpty is false)
                    {
                        Instantiate(ItemDataRepository.instance.equipments[slotData.itemID].model,
                            bodyParts[slotIndex].Value);
                    }
                };
            }
        }
    }

}