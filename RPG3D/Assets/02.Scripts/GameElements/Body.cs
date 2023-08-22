using RPG.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BodyPart
{
    None,
    Head,
    Top,
    Bottom,
    Feet,
    RightHand,
    LeftHand,
    TwoHand
}


public class Body : MonoBehaviour
{
    public List<UKeyValuePair<BodyPart, Transform>> bodyParts;

    private void Start()
    {
        if(DataModelManager.instance.TryGet(out ItemsEquippendData itemsEquippendData))
        {
            itemsEquippendData.slotDatum.onItemChanged += (slotIndex, slotData)
               =>
            {
                //���� �ı�
                if (bodyParts[slotIndex].Value.childCount is 1)
                {
                    Destroy(bodyParts[slotIndex].Value.GetChild(0).gameObject);
                }


                //���� ���� ��
                if(slotData.isEmpty is false)
                {
                    Instantiate(ItemDataRepository.instance.equipments[slotData.itemID].model,
                        bodyParts[slotIndex].Value);
                }
            };
        }
    }
}
