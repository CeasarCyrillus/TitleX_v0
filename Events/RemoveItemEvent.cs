using Inventory;
using Inventory.Items;

namespace Events
{
    public readonly struct RemoveItemEvent
    {
        public readonly ItemController item;

        public RemoveItemEvent(ItemController item)
        {
            this.item = item;
        }
    }
}