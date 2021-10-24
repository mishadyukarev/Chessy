namespace Scripts.Game
{
    internal struct CellWeatherDataCom
    {
        internal bool EnabledCloud;
        internal CloudWidthTypes CloudWidthType;
        internal bool IsCenter => CloudWidthType != default;
    }
}