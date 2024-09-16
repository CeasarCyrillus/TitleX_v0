using Events;
using UnityEngine;
using Weapons.Gun.Parts;
using Weapons.Gun.Parts.data;
using Weapons.Gun.Parts.Data;

namespace Weapons
{
    public class Glock17Item: MonoBehaviour
    {
        private void Start()
        {
            OnStart();
        }

        private async void OnStart()
        {
            var glockAmmo = new AmmoData(115, 5, 9.03f, 9.9f);
            
            var barrelData = new BarrelData(200f, 11.4f);
            var barrel = new Barrel(barrelData);
            
            var triggerData = new TriggerData();
            var trigger = new Trigger(triggerData);

            var magazineData = new MagazineData(450, 10000000);
            var magazine = new Magazine(magazineData);
            magazine.Load(glockAmmo, 10000000);
            
            var gunParts = new GunParts(trigger, barrel);
            await EventBus.Instance.Publish(new GunPickupEvent(gunParts));
            await EventBus.Instance.Publish(new ChangeAmmoEvent(magazine));
        }
    }
}