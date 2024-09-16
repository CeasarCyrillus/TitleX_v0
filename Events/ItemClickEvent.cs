using Inventory;
using Inventory.Items;

namespace Events
{
    public struct ItemClickEvent
    {
        public readonly ItemController PhysicalItem;

        public ItemClickEvent(ItemController physicalItem)
        {
            this.PhysicalItem = physicalItem;
        }
    }
}