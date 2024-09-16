using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lib
{
    public class GlobalAssets: MonoBehaviour
    {
        [SerializeField] public Material validPlacement;
        [SerializeField] public Material inValidPlacement;

        public static GlobalAssets Instance { get; private set; }
        private void Awake()
        {

            Instance = this;
        }
    }
}