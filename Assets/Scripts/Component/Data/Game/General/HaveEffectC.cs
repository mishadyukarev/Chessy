namespace Game.Game
{
    public struct HaveEffectC : IUnitCell, IFireCell, ICloudCell
    {
        public bool Have;

        public void Disable() => Have = false;
        public void Enable() => Have = true;

        public void Sync(in bool have) => Have = have;
    }
}