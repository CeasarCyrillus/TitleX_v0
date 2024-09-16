namespace Lib
{
    public static class UnitConverter
    {
        public static float GrainsToKilograms(int grains)
        {
            return grains * 0.00006479891f;
        }

        public static float CentimeterToMeters(float centimeters)
        {
            return centimeters * 0.01f;
        }

        public static float MillimeterToMeter(float millimeter)
        {
            return millimeter * 0.001f;
        }
        
        public static float CentimetersToScale(float centimeters)
        {
            return centimeters / 100f;
        }
    }
}