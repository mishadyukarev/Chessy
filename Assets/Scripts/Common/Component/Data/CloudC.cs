namespace Chessy.Common
{
    public struct CloudC
    {
        public byte Center;

        public CloudC(in byte idxCell) => Center = idxCell;
    }
}