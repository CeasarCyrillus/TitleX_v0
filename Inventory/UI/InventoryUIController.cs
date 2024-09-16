using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events;
using Inventory.Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory.UI
{
    public class InventoryUIController: ItemUiList
    {
        private List<ItemController> items = new();

        private void SetupRow(ItemController item, TemplateContainer rowTemplate)
        {
            rowTemplate.Q<Label>("ItemName").text = item.itemName;
            rowTemplate.Q<Button>("ItemRowButton").clicked += async () =>
            {
                await EventBus.Instance.Publish(new ItemClickEvent(item));
            };
        }

        private Task OnUpdateInventory(UpdateInventoryEvent updateInventoryEvent)
        {
            items = updateInventoryEvent
                .items
                .Select(item => item.Value)
                .ToList();
            UpdateUI(items, SetupRow);
            return Task.CompletedTask;
        }
        
        private Task OnToggleInventory(InputToggleInventoryEvent toggleInventoryEvent)
        {
            ShowUi(toggleInventoryEvent.isOpen);
            return Task.CompletedTask;
        }
        
        private new void Start()
        {
            base.Start();
            EventBus.Instance.Subscribe<UpdateInventoryEvent>(OnUpdateInventory);
            EventBus.Instance.Subscribe<InputToggleInventoryEvent>(OnToggleInventory);
        }
        
    }
}