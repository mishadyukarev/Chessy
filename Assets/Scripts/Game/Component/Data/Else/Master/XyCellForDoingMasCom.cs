namespace Scripts.Game
{
    public struct XyCellForDoingMasCom
    {
        private byte[] _xyCellForDoing;

        public byte[] XyCellForDoing
        {
            get => (byte[])_xyCellForDoing.Clone();
            set => _xyCellForDoing = (byte[])value.Clone();
        }

        public XyCellForDoingMasCom(byte[] xy) => _xyCellForDoing = xy;
    }
}
