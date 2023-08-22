using RPG.Data;
using System.Collections.ObjectModel;

namespace RPG.DependencySources
{
    public class ItemsEquippedPresenter
    {
        public class ItemsEquipeedSource
        {
            public ObservableCollection<ItemsEquippendData.ItemEquippedSlotData> itemsEquippedSlotDatum;

            public ItemsEquipeedSource()
            {
                if(DataModelManager.instance.TryGet(out ItemsEquippendData source))
                {
                    itemsEquippedSlotDatum
                        = new ObservableCollection<ItemsEquippendData.ItemEquippedSlotData>(source.slotDatum);
                    source.slotDatum.onItemChanged += (slotIndex, slotData) =>
                    {
                        itemsEquippedSlotDatum[slotIndex] = slotData;
                    };
                }
            }
        }
        public ItemsEquipeedSource itemsEquipeedSource;

        public ItemsEquippedPresenter()
        {
            itemsEquipeedSource = new();
        }
    }
}


//https://tsyang.tistory.com/40
//https://wlsdn629.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0%EC%97%90%EC%84%9C-ChatGPT-%EC%9D%B4%EC%9A%A9%ED%95%98%EA%B8%B0-2?category=1051398