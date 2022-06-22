namespace Chessy.Game
{
    public struct XyCellC
    {
        public readonly byte[] Xy;

        internal XyCellC(in byte[] xy) => Xy = xy;
    }
}
