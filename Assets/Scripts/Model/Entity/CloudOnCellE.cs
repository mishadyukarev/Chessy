using Chessy.Model.Component;

namespace Chessy.Model.Entity
{
    public sealed class CloudOnCellE
    {
        public readonly CloudC CloudC = new();
        public readonly WhereViewIdxCellC WhereSkinAndWhereDataInfoC = new();
        public readonly ShiftingObjectC ShiftingC = new();

        internal void Dispose()
        {
            CloudC.Dispose();
            WhereSkinAndWhereDataInfoC.Dispose();
            ShiftingC.Dispose();
        }

        internal void Clone(in CloudOnCellE newCloudOnCellE)
        {
            CloudC.Clone(newCloudOnCellE.CloudC);
            WhereSkinAndWhereDataInfoC.Clone(newCloudOnCellE.WhereSkinAndWhereDataInfoC);
            ShiftingC.Clone(newCloudOnCellE.ShiftingC);
        }
    }
}