using System.Collections.Generic;
using System.Linq;
using Inventory.Items;
using Inventory.Items.WorkBenchItems;
using UnityEngine;

namespace Crafting
{
    public static class WorkbenchUtil
    {
        public static bool TryGetWorkbenchMousePosition(Camera camera, out Vector3 mousePosition)
        {
            mousePosition = Vector3.zero;
            var ray = GetCameraRay(camera);
            if (Physics.Raycast(ray, out var workBenchHit, maxRayDistance, LayerMask.GetMask("WorkBench")))
            {
                mousePosition = workBenchHit.point;
                return true;
            };

            return false;
        }

        public static bool TryGetWorkBenchItemClicked(List<WorkBenchItem> availableWorkBenchItems, Camera camera, out WorkBenchItem workBenchItem)
        {
            workBenchItem = null;
            var ray = GetCameraRay(camera);
            if(Physics.Raycast(ray, out var hit, maxRayDistance, LayerMask.GetMask("Item")))
            {
                workBenchItem = hit.collider.GetComponent<WorkBenchItem>();
                return availableWorkBenchItems.Contains(workBenchItem);

            }
            return workBenchItem;
        }
        
        private static readonly float snappingRadius = 0.05f;
        private static readonly float maxRayDistance = 100f;

        public static bool TryGetClosestSnappingPoint(WorkBenchItem[] availableWorkBenchItems, Vector3 position,
            out (Vector3 closestPoint, WorkBenchItem item) snappingPoint)
        {
            snappingPoint = (Vector3.zero, null);
            var closestWorkBenchItem = availableWorkBenchItems
                .Select(item =>
                {
                    var closestPoint = item.itemCollider.ClosestPoint(position);
                    var distance = Vector3.Distance(position, closestPoint);
                    return new
                    {
                        item,
                        closestPoint,
                        distance,
                    };
                })
                .Where(result => result.distance < snappingRadius)
                .OrderBy(result => result.distance)
                .FirstOrDefault();

            if (closestWorkBenchItem == null) return false;
            snappingPoint = (closestWorkBenchItem.closestPoint, closestWorkBenchItem.item);
            return true;
        }
        
        public static void MoveToPosition(ItemController itemController, Vector3 newPosition)
        {
            itemController.transform.position = newPosition;
        }

        public static void RotateTowards(WorkBenchItem workbenchItem, Vector3 positionToFace)
        {
            var directionToFace = (positionToFace - workbenchItem.itemController.transform.position).normalized;
            if(directionToFace != Vector3.zero)
                workbenchItem.transform.forward = directionToFace;
        }

        private static Ray GetCameraRay(Camera camera)
        {
            var mouseScreenPosition = Input.mousePosition;
            var ray = camera.ScreenPointToRay(mouseScreenPosition);
            return ray;
        }
    }
}