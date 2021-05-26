using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System")]
    public class ObjectInInventory : ScriptableObject
    {
        public List<InventorySlot> inventoryContainer = new List<InventorySlot>();
    
        public void AddItem(ItemObject item, int amount)
        {
            bool hasItem = false;
        
            //loops through the List
            for (int i = 0; i < inventoryContainer.Count; i++)
            {
                //if it has the item, add it to the count
                if (inventoryContainer[i].item == item)
                {
                    inventoryContainer[i].AddAmount(amount);
                    hasItem = true;
                    break;
                }
            }

            if (!hasItem)
            {
                inventoryContainer.Add(new InventorySlot(item, amount));
            }
        }
    }


    [Serializable]
    public class InventorySlot
    {
        public ItemObject item;
        public int amount;

        public InventorySlot(ItemObject item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}