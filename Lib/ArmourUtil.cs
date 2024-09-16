using System.Collections.Generic;
using Armour;
using UnityEngine;

namespace Lib
{
    public class ArmourUtil: MonoBehaviour
    {
        [SerializeField] private float globalArmourValueModifier = 1f;
        private Dictionary<Materials, float> materialArmourValue;
        
        private static ArmourUtil _instance;
        public static ArmourUtil Instance => _instance;
        
        private void Awake()
        {
            _instance = this;
            materialArmourValue = new Dictionary<Materials, float>()
            {
                [Materials.Skin] = 1f,
                [Materials.Leather] = 2f,
                [Materials.Iron] = 20f,
                [Materials.Steel] = 40f,
            };
        }

        public float GetArmourValue(ArmourData armourData)
        {
            var durability = armourData.material;
            return armourData.thicknessMM * materialArmourValue[armourData.material];
        }
    }
}