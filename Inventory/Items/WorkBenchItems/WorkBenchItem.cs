using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.Items.WorkBenchItems
{
    public class WorkBenchItem: BaseItemController
    {
        [SerializeField] private List<ItemType> canSnapToList;
        [SerializeField] private bool canSnapToAll = true;
        [SerializeField] private bool rotateToMatchSnapItemSurface;
        
        private readonly HashSet<ItemType> canSnapTo = new();
        private Outline outline;
        private Collider rootCollider;
        public Collider itemCollider { get; private set; }
        private bool isValid;

        public bool CanSnapTo(ItemType itemType)
        {
            return canSnapToAll || canSnapTo.Contains(itemType);
        }
        
        public new void Awake()
        {
            base.Awake();
            itemCollider = GetComponent<Collider>();
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineWidth = 0.1f;
            outline.enabled = false;
            canSnapTo.AddRange(canSnapToList);
        }

        public void StartHolding()
        {
        }
        
        public void StopHolding()
        {
        }

        public bool RotateToMatchSnapItemSurface()
        {
            return rotateToMatchSnapItemSurface;
        }
    }
}