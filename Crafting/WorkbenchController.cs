using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events;
using Inventory;
using Inventory.Items;
using Inventory.Items.WorkBenchItems;
using UnityEngine;

namespace Crafting
{
    public class WorkbenchController: MonoBehaviour
    {
        [SerializeField] private Camera craftingCamera;
        private WeaponCraftingUi itemListUi;
        private WorkBenchItem heldItem;
        private WorkBenchItem rootItem;
        private readonly List<WorkBenchItem> allWorkbenchItems = new();
        

        public void Start()
        {
            itemListUi = GetComponent<WeaponCraftingUi>();
        }

        public void AddItem(ItemController item)
        {
            item.ToItemState(ItemState.WorkBench);
            
            item.transform.parent = transform;
            item.transform.position = transform.position;
            item.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - item.transform.rotation.eulerAngles);
            var workBenchItem  = item.ItemInState<WorkBenchItem>(ItemState.WorkBench);
            
            PickupNewItem(workBenchItem);
            
            allWorkbenchItems.Add(workBenchItem);
            if (!rootItem)
            {
                rootItem = heldItem;
            }
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            var allItems = InventoryController.Instance.GetItems();
            if (allWorkbenchItems.Count == 0)
            {
                itemListUi.UpdateUI(allItems, item => item.itemType == ItemType.GunBody);
            }
            else
            {
                itemListUi.UpdateUI(allItems, _ => true);
            }
        }

        private void PickupNewItem(WorkBenchItem item)
        {
            DropItem();
            heldItem = item;
            heldItem.StartHolding();
        }
        
        private void DropItem()
        {
            heldItem?.StopHolding();
            heldItem = null;
        }
        
        private void Update()
        {
            if (!WorkbenchUtil.TryGetWorkbenchMousePosition(craftingCamera, out var mousePosition)) 
                return;

            var mouseButtonClicked = Input.GetButtonDown("Fire1");
            if (heldItem)
            {
                WorkbenchUtil.MoveToPosition(heldItem.itemController, mousePosition);
                if (mouseButtonClicked)
                {
                    DropItem();
                }

                return;
            }
            
            if (!heldItem && mouseButtonClicked && WorkbenchUtil.TryGetWorkBenchItemClicked(allWorkbenchItems, craftingCamera, out var clickedWorkbenchItem))
            {
                PickupNewItem(clickedWorkbenchItem);
                return;
            }
            
            
            // TODO: snap closes point to closes point
            /*
            var foundSnappingPoint = WorkbenchUtil.TryGetClosestSnappingPoint(
                ValidItemsToSnapTo(),
                mousePosition, out var snappingPoint);
            
            if (foundSnappingPoint)
            {
                var mySnappingPoint = heldItem.itemCollider.ClosestPoint(snappingPoint.item.itemController.transform.position);
                var positionSnapPointDelta = heldItem.itemController.transform.position - mySnappingPoint; 
                WorkbenchUtil.MoveToPosition(heldItem.itemController,  positionSnapPointDelta + snappingPoint.closestPoint);
                if (heldItem.RotateToMatchSnapItemSurface())
                {
                    //WorkbenchUtil.RotateTowards(heldItem, mousePosition);   
                }
            }
            else
            {
                WorkbenchUtil.MoveToPosition(heldItem.itemController, mousePosition);
            }
            */
        }

        private WorkBenchItem[] ValidItemsToSnapTo()
        {
            return allWorkbenchItems
                .Where(item => heldItem.CanSnapTo(item.itemController.itemType))
                .Where(item => item != heldItem)
                .ToArray();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventBus.Instance.Subscribe<InputInteractEvent>(OnInteract);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventBus.Instance.Unsubscribe<InputInteractEvent>(OnInteract);
                craftingCamera.enabled = false;
                itemListUi.ShowUi(false);
            }
        }

        private Task OnInteract(InputInteractEvent _)
        {
            UpdateUI();
            
            itemListUi.ShowUi(true);
            craftingCamera.enabled = true;
            EventBus.Instance.Unsubscribe<InputInteractEvent>(OnInteract);
            return Task.CompletedTask;
        }

        public void OnConnectionModeChange(bool isInConnectionMode)
        {
            foreach (var workbenchItem in allWorkbenchItems)
            {
                var connectors = workbenchItem.itemController.GetComponentsInChildren<Connector>(true);
                foreach (var connector in connectors)
                {
                    if (isInConnectionMode)
                    {
                        connector.OnEnterConnectionMode();
                    }
                    else
                    {
                        connector.OnExitConnectionMode();
                    }
                }
            }
        }
    }
}