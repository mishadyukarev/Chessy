namespace Assets.Scripts.ECS.Component.Game.Master
{
    internal struct XyCellForDoingMasCom
    {
        private int[] _xyCellForDoing;

        internal int[] XyCellForDoing
        {
            get => (int[])_xyCellForDoing.Clone();
            set => _xyCellForDoing = (int[])value.Clone();
        }

        internal XyCellForDoingMasCom(int[] xy) => _xyCellForDoing = xy;
    }
}
