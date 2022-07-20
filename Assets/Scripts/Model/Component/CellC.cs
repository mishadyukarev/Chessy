namespace Chessy.Model
{
    public sealed class CellC
    {
        public readonly bool IsBorder;
        public readonly int InstanceID;

        internal CellC(in bool isBorder, in int instanceID)
        {
            IsBorder = isBorder;
            InstanceID = instanceID;
        }
    }
}