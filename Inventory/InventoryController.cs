using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events;
using Inventory.Items;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    public class InventoryController: MonoBehaviour
    {
        private readonly Dictionary<string, ItemController> items = new();
        private GameObject inventoryGameObject;

        public static InventoryController Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            inventoryGameObject = GameObject.FindGameObjectWithTag("Inventory");
            EventBus.Instance.Subscribe<AddItemEvent>(OnAddItemEvent);
            EventBus.Instance.Subscribe<RemoveItemEvent>(OnRemoveItemEvent);
            EventBus.Instance.Subscribe<DropItemEvent>(OnDropItemEvent);
        }

        private Task OnDropItemEvent(DropItemEvent dropItemEvent)
        {
            return EventBus.Instance.Publish(new RemoveItemEvent(dropItemEvent.item));
        }

        private Task OnRemoveItemEvent(RemoveItemEvent removeItemEvent)
        {
            items.Remove(removeItemEvent.item.id);
            return EventBus.Instance.Publish(new UpdateInventoryEvent(items));
        }

        private Task OnAddItemEvent(AddItemEvent addItemEvent)
        {
            var item = addItemEvent.item;
            item.transform.parent = inventoryGameObject.transform;
            item.transform.position = inventoryGameObject.transform.position;
            item.transform.rotation = Quaternion.identity;
            items.Add(item.id, item);
            return EventBus.Instance.Publish(new UpdateInventoryEvent(items));
        }

        public List<ItemController> GetItems()
        {
            return items.Values.ToList();
        }
        
        public List<ItemController> GetItems(Func<ItemController, bool> filter)
        {
            return items.Values.Where(filter).ToList();
        }
    }
}