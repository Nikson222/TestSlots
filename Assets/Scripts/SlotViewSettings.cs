using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "SlotViewSettings", menuName = "ScriptableObjects/SlotViewSettings", order = 1)]
    public class SlotViewSettings : ScriptableObject
    {
        public List<SlotItemSettings> ItemSettings;
        
        public int MinRollingCycle;
        public int MaxRollingCycle;

        public Sprite GetItemSprite(SlotItem item)
        {
            foreach (var Item in ItemSettings)
            {
                if(Item.Item == item)
                    return Item.Sprite;
            }

            return ItemSettings.First().Sprite;
        }
    }

    [Serializable]
    public class SlotItemSettings
    {
        public SlotItem Item;
        public Sprite Sprite;
    }
}