using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inventory.Items
{
    public class ItemController: MonoBehaviour
    {
        [SerializeField] public string id;
        [SerializeField] public string itemName;
        [SerializeField] public ItemType itemType;
        
        public ItemState activeState { get; private set; }

        private readonly Dictionary<ItemState, BaseItemController> baseItemControllers = new();
        private void Start()
        {
            if (id == "")
            {
                id = GUID.Generate().ToString();
            }

            var baseItemControllersArray = GetComponentsInChildren<BaseItemController>(true);
            foreach (var baseItemController in baseItemControllersArray)
            {
                baseItemControllers.TryAdd(baseItemController.itemState, baseItemController);
            }
        }

        public void ToItemState(ItemState newState)
        {
            activeState = newState;
            foreach (var baseItem in baseItemControllers.Values)
            {
                baseItem.gameObject.SetActive(baseItem.itemState == newState);
            }
        }

        public T ItemInState<T>(ItemState itemState) where T : BaseItemController
        {
            return baseItemControllers[itemState] as T;
        }
    }

    public enum ItemState
    {
        None,
        Physical,
        Inventory,
        WorkBench,
    }
}