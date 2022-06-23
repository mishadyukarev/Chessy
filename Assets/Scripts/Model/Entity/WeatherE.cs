namespace Chessy.Model.Model.Entity
{
    public struct WeatherE
    {
        public WindC WindC;
        public SunSideTypes SunSideT { get; set; }
        public byte CellIdxCenterCloud { get; internal set; }
    }
}