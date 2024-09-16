using System;

namespace Lib
{
    public enum Materials
    {
        None,
        Skin,
        Textile,
        Leather,
        Wood,
        Iron,
        Ceramic,
        Steel,
        Titanium
    }
    
    
    [Serializable]
    public struct MaterialModifier
    {
        public Materials material;
        public float factor;
    }
}