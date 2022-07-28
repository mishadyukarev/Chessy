using Chessy.Model.Component;

namespace Chessy.Model.Entity
{
    sealed class CloudOnCellE
    {
        public readonly CloudC CloudC = new();
        public readonly WhereViewIdxCellC WhereSkinAndWhereDataInfoC = new();
        public readonly ShiftingObjectC ShiftingC = new();
    }
}