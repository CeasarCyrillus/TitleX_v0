using Events;
using Inventory;
using Inventory.Items;
using UnityEngine;

namespace Player
{
    public class PlayerPickupItemZone: MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ItemController>(out var item))
            {
                if (other.TryGetComponent<Rigidbody>(out var itemRb))
                {
                    itemRb.velocity = Vector3.zero;
                }
                EventBus.Instance.Publish(new AddItemEvent(item));
            }
        }
    }
}