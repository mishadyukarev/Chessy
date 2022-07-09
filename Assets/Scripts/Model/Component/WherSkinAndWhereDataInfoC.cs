namespace Chessy.Model.Component
{
    public struct WherSkinAndWhereDataInfoC
    {
        public byte SkinIdxCell { get; internal set; }
        public byte DataIdxCell { get; internal set; }

        public bool HaveSkin => SkinIdxCell != 0;
        public bool HaveData => DataIdxCell != 0;
    }
}