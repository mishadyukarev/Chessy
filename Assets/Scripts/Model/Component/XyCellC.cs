namespace Chessy.Model
{
    public struct XyCellC
    {
        public readonly byte[] Xy;

        internal XyCellC(in byte[] xy) => Xy = xy;
    }
}
