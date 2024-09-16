using Inventory;
using Inventory.Items;

namespace Events
{
    public readonly struct AddItemEvent
    {
        public readonly ItemController item;

        public AddItemEvent(ItemController item)
        {
            this.item = item;
        }
    }
}