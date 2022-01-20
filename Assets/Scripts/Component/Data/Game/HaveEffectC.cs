namespace Game.Game
{
    public struct HaveEffectC : IUnitCellE, IFireCell, ICloudCell, IUnitStatCellE
    {
        public bool Have;

        public void Disable() => Have = false;
        public void Enable() => Have = true;
    }
}