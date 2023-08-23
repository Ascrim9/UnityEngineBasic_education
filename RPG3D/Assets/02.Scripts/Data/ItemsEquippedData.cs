using System;
using RPG.Collections;
using RPG.GameElements;

namespace _02.Scripts.Data
{
    public class ItemsEquippendData : IDataModel  
    {
        public int id { get; set; }


        [Serializable]
        public class ItemEquippedSlotData
        {
            public bool isEmpty => itemID <= 0;

            public BodyPart part;
            public int itemID;
        }

        public ObservableCollection<ItemEquippedSlotData> slotDatum;


        public void Init()
        {
            slotDatum.onCollectionChanged += () => DataModelManager.instance.Save<ItemsEquippendData>();
        }

        public IDataModel ResetWithDefaults()
        {
            slotDatum = new ObservableCollection<ItemEquippedSlotData>();

            foreach (BodyPart bodyPart in Enum.GetValues(typeof(BodyPart))) 
            {
                slotDatum.Add(new ItemEquippedSlotData() { part = bodyPart, itemID = 0 });
            }

            return this;
        }
    }

}