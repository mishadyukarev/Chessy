namespace Scripts.Game
{
    internal struct XyCellForDoingMasCom
    {
        private byte[] _xyCellForDoing;

        internal byte[] XyCellForDoing
        {
            get => (byte[])_xyCellForDoing.Clone();
            set => _xyCellForDoing = (byte[])value.Clone();
        }

        internal XyCellForDoingMasCom(byte[] xy) => _xyCellForDoing = xy;
    }
}
