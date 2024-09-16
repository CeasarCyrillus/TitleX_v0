using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class ModifiersUtil
    {
        public static Dictionary<Materials, MaterialModifier> InitializeMaterialModifierDict(List<MaterialModifier> allMaterialModifiers)
        {
            var dict = new Dictionary<Materials, MaterialModifier>();
            foreach (var modifier in allMaterialModifiers)
            {
                dict.Add(modifier.material, modifier);
            }

            return dict;
        }

        public static Func<float, float> GetMaterialModifier(Dictionary<Materials, MaterialModifier> allModifiers, Materials material)
        {
            if (allModifiers.TryGetValue(material, out var modifier))
            {
                return durability => durability * modifier.factor;
            }
            
            Debug.LogError("Could not find material " + material);
            return _ => 1f;
        }

        public static float ApplyModifiers(float value, List<Func<float, float>> modifiers)
        {
            var effectiveValue = value;
            foreach (var modifier in modifiers)
            {
                effectiveValue *= modifier(effectiveValue);
            }
            return effectiveValue;
        }
    }
}