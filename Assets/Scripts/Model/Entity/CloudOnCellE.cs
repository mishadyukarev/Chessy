using Chessy.Model.Component;

namespace Chessy.Model.Entity
{
    public struct CloudOnCellE
    {
        public CloudC CloudC;
        public WherSkinAndWhereDataInfoC WhereSkinAndWhereDataInfoC;
        public ShiftingObjectC ShiftingC;
        public PositionC PositionC;

        internal void Dispose()
        {
            CloudC.Dispose();
            WhereSkinAndWhereDataInfoC = default;
            ShiftingC = default;
            PositionC = default;
        }
    }
}