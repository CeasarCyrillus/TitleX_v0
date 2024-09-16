using System;
using Lib;
using UnityEngine;
using UnityEngine.Serialization;

namespace Armour
{
    [Serializable]
    public class ArmourData
    {
        [SerializeField] public float thicknessMM;
        [SerializeField] public Materials material;
    }
    
}