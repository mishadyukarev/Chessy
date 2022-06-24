namespace Chessy.Model
{
    public struct CellC
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