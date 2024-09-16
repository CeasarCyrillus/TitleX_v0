using UnityEngine;

namespace Inventory.Items.WorkBenchItems
{
    public class Connector: WorkBenchItem
    {
        private MeshRenderer meshRenderer;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void OnEnterConnectionMode()
        {
            if (itemController.itemType == ItemType.GunTrigger)
            {
                meshRenderer.enabled = true;
            }
        }
        
        public void OnExitConnectionMode()
        {
            meshRenderer.enabled = false;
        }

        public void OnNextConnection(ItemType lastConnection)
        {
            switch (lastConnection)
            {
                case ItemType.GunTrigger:
                    meshRenderer.enabled = itemController.itemType == ItemType.GunBarrel;
                    break;
                case ItemType.GunBarrel:
                    meshRenderer.enabled = itemController.itemType == ItemType.GunMagazineSpot;
                    break;
                default:
                    meshRenderer.enabled = false;
                    break;
            }
        }
    }
}