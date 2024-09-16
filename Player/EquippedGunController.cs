using System.Collections.Generic;
using System.Threading.Tasks;
using Events;
using UnityEngine;
using Weapons.Gun.Parts.Controller;
using Weapons.Gun.Parts.data;

namespace Player
{
    [RequireComponent(typeof(AmmoController))]
    [RequireComponent(typeof(GunController))]
    public class EquippedGunController: MonoBehaviour
    {
        private readonly List<GunParts> availableGuns = new();
        private GunController gunController;
        private AmmoController ammoController;

        private void Awake()
        {
            ammoController = GetComponent<AmmoController>();
            gunController = GetComponent<GunController>();
            EventBus.Instance.Subscribe<GunPickupEvent>(OnGunPickup);
            EventBus.Instance.Subscribe<InputNumericKeysEvent>(OnNumericKeysInput);
            EventBus.Instance.Subscribe<InputClickEvent>(OnClickEvent);
            EventBus.Instance.Subscribe<InputReloadEvent>(OnReloadEvent);
        }

        private Task OnClickEvent(InputClickEvent clickEvent)
        {
            gunController.Shoot();
            return Task.CompletedTask;
        }

        private async Task OnNumericKeysInput(InputNumericKeysEvent numericKeysEvent)
        {
            var index = numericKeysEvent.number - 1;
            if (index <= availableGuns.Count - 1)
            {
                var gun = availableGuns[index];
                gunController.SetParts(gun);
                await EventBus.Instance.Publish(new InputReloadEvent());
            }
        }

        private Task OnReloadEvent(InputReloadEvent _)
        {
            gunController?.Reload(ammoController.currentAmmo);
            return Task.CompletedTask;
        }

        private Task OnGunPickup(GunPickupEvent gunPickupEvent)
        {
            availableGuns.Add(gunPickupEvent.gunParts);
            return Task.CompletedTask;
        }
    }
}