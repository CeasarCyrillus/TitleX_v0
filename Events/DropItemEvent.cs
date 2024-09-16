using Inventory;
using Inventory.Items;
using UnityEngine;

namespace Events
{
    public readonly struct DropItemEvent
    {
        public readonly ItemController item;

        public DropItemEvent(ItemController item)
        {
            this.item = item;
        }
    }
}