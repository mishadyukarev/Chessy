namespace Chessy.Model
{
    public struct WindC
    {
        internal DirectTypes DirectType;
        internal byte Speed;

        public DirectTypes DirectT => DirectType;
        public byte SpeedP => Speed;
    }
}