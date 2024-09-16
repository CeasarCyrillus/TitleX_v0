using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events;
using Inventory.Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory.UI
{
    public class ItemUiList: MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset itemRowTemplate;
        [SerializeField] protected UIDocument uiDocument;
        private readonly Queue<VisualElement> uiItemsRows = new();
        
        private VisualElement root;
        private VisualElement inventoryContainer;


        protected void UpdateUI(List<ItemController>items, Action<ItemController, TemplateContainer> setupRow)
        {
            while (uiItemsRows.TryDequeue(out var row))
            {
                row.RemoveFromHierarchy();
            }
            
            foreach (var item in items)
            {
                var itemUiRow = itemRowTemplate.Instantiate();
                setupRow(item, itemUiRow);
                inventoryContainer.Add(itemUiRow);
                uiItemsRows.Enqueue(itemUiRow);
            }
        }

        public void ShowUi(bool show)
        {
            root.visible = show;
        }
        
        public void Start()
        {
            root = uiDocument.rootVisualElement;
            inventoryContainer = root.Q<VisualElement>("InventoryList");
            root.visible = false;
        }
    }
}