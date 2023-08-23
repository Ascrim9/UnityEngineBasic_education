using RPG.UI;
using UnityEditor;
using UnityEngine;

namespace _02.Scripts.Data
{
    [CreateAssetMenu(fileName = "new EquipmentItemData", menuName = "RPG/Data/Create EquipmentItemData")]
    public class EquipmentItemData : UsableItemData
    {
        public BodyPart bodyPart;
        public void Use(InventorySlot slot)
        {
            if (DataModelManager.instance.TryGet(out InventoryData inventoryData)
                && DataModelManager.instance.TryGet(out ItemsEquippendData itemsEquippedData))
            {
                
                var equipmentItemData = ItemDataRepository.instance[slot.itemID] as EquipmentItemData;

                if (equipmentItemData is not null)
                {
                    var equippedSlotData =
                        itemsEquippedData.slotDatum[(int)equipmentItemData.bodyPart];
                }

                var inventorySlotData = inventoryData.equipmentSlotDatum[slot.slotIndex];
                
                if (equipmentItemData is not null)
                {
                    inventoryData.equipmentSlotDatum[slot.slotIndex] = new InventoryData.EquipmentSlotData()
                    {
                        itemId = equipmentItemData.id,
                        itemNum = inventorySlotData.itemID,
                        enhanceLevel =  inventorySlotData.enhandlavel,
                    };
                }
                
            }
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
