using System.Collections.Generic;
using Inventory;
using Inventory.Items;

namespace Events
{
    public readonly struct UpdateInventoryEvent
    {
        public readonly Dictionary<string, ItemController> items;

        public UpdateInventoryEvent(Dictionary<string, ItemController> items)
        {
            this.items = items;
        }
    }
}