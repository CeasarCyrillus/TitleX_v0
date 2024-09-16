using System;
using UnityEngine;

namespace Inventory.Items
{
    public class BaseItemController: MonoBehaviour
    {
        [SerializeField] public ItemState itemState;
        public ItemController itemController { get; private set; }

        public void Awake()
        {
            itemController = transform.parent.GetComponent<ItemController>();
        }
    }
}