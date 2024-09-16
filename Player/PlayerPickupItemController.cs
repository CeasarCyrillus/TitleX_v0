using System.Collections.Generic;
using System.Linq;
using Events;
using Inventory.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerPickupItemController: MonoBehaviour
    {
        private readonly List<Rigidbody> itemRigidBodies = new();
        private readonly List<PhysicalItem> items = new();
        [SerializeField] private float acceleration;
        [SerializeField] private float maxPickupDistance;

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PhysicalItem>(out var physicalItem) && other.TryGetComponent<Rigidbody>(out var itemRb))
            {
                items.Add(physicalItem);
                itemRigidBodies.Add(itemRb);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PhysicalItem>(out var physicalItem) && other.TryGetComponent<Rigidbody>(out var itemRb))
            {
                items.Remove(physicalItem);
                if (itemRigidBodies.Remove(itemRb))
                {
                    itemRb.velocity = Vector3.zero;
                }
            }
        }

        public void Update()
        {
            if (!Input.GetKey(KeyCode.E)) 
                return;
            
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var itemRb = itemRigidBodies[i];
                
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (maxPickupDistance >= distance)
                {
                    var itemController = item.itemController;
                    itemController.ToItemState(ItemState.Inventory);
                    EventBus.Instance.Publish(new AddItemEvent(itemController));
                    
                    items.RemoveAt(i);
                    itemRigidBodies.RemoveAt(i);
                }
                else
                {
                    var direction = (transform.position - itemRb.transform.position).normalized;
                    itemRb.velocity += direction * acceleration;
                }
            }
        }
    }
}