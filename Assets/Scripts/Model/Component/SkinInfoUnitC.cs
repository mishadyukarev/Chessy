namespace Chessy.Model.Component
{
    public struct SkinInfoUnitC
    {
        public byte SkinIdxCell { get; internal set; }
        public byte DataIdxCell { get; internal set; }


        public bool HaveSkin => SkinIdxCell != 0;
        public bool HaveData => DataIdxCell != 0;
    }
}