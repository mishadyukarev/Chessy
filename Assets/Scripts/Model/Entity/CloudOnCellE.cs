using Chessy.Model.Component;

namespace Chessy.Model.Entity
{
    public sealed class CloudOnCellE
    {
        public readonly CloudC CloudC = new CloudC();
        public readonly WhereViewIdxCellC WhereSkinAndWhereDataInfoC = new WhereViewIdxCellC();
        public readonly ShiftingObjectC ShiftingC = new ShiftingObjectC();
        public readonly PositionC PositionC = new PositionC();

        internal void Dispose()
        {
            CloudC.Dispose();
            WhereSkinAndWhereDataInfoC.Dispose();
            ShiftingC.Dispose();
            PositionC.Dispose();
        }

        internal void Clone(in CloudOnCellE newCloudOnCellE)
        {
            CloudC.Clone(newCloudOnCellE.CloudC);
            WhereSkinAndWhereDataInfoC.Clone(newCloudOnCellE.WhereSkinAndWhereDataInfoC);
            ShiftingC.Clone(newCloudOnCellE.ShiftingC);
            PositionC.Clone(newCloudOnCellE.PositionC);
        }
    }
}