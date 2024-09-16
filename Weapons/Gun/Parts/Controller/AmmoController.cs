using System.Threading.Tasks;
using Events;
using UnityEngine;

namespace Weapons.Gun.Parts.Controller
{
    public class AmmoController: MonoBehaviour
    {
        public Gun.Parts.Magazine currentAmmo { get; private set; }

        private void Awake()
        {
            EventBus.Instance.Subscribe<ChangeAmmoEvent>(OnChangeAmmoEvent);
        }

        private async Task OnChangeAmmoEvent(ChangeAmmoEvent ammoChangeEvent)
        {
            currentAmmo = ammoChangeEvent.magazine;
            await EventBus.Instance.Publish(new InputReloadEvent());
        }
    }
}