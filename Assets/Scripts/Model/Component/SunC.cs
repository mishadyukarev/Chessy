namespace Chessy.Model
{
    public struct SunC
    {
        internal SunSideTypes SunSideType;

        public SunSideTypes SunSideT => SunSideType;
        public int SecondsForChangingSideSun { get; internal set; }
    }
}