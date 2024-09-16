using System;
using System.Collections.Generic;
using Events;
using Inventory.Items;
using Inventory.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using EventBus = Events.EventBus;

namespace Crafting
{
    public class WeaponCraftingUi: ItemUiList
    {
        [SerializeField] private WorkbenchController workbench;
        private bool isInConnectionMode;
        new void Start()
        {
            base.Start();
            uiDocument.rootVisualElement.Q<Button>("ToggleConnectionModeBtn").clicked += () =>
            {
                isInConnectionMode = !isInConnectionMode;
                workbench.OnConnectionModeChange(isInConnectionMode);
            };
        }
        
        private Action<ItemController, TemplateContainer> SetupRow(Func<ItemController, bool> isActive) =>
            (item, rowTemplate) =>
            {
                rowTemplate.Q<Label>("ItemName").text = item.itemName;
                if (isActive(item))
                {
                    rowTemplate.Q<Button>("ItemRowButton").clicked += OnRowClick(item);
                }
                else
                {
                    rowTemplate.Q<Label>("ItemName").style.color = Color.grey;
                }
            };

        private Action OnRowClick(ItemController item)
        {
            return async () =>
            {
                await EventBus.Instance.Publish(new RemoveItemEvent(item));
                workbench.AddItem(item);
            };
        }

        public void UpdateUI(List<ItemController> items, Func<ItemController, bool> isActive)
        {
            base.UpdateUI(items, SetupRow(isActive));
        }
        
    }
}