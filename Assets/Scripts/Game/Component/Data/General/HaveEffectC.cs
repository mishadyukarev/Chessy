namespace Game.Game
{
    public struct HaveEffectC : IUnitCell, IFireCell
    {
        public bool Have;

        public void Disable() => Have = false;
        public void Enable() => Have = true;
    }
}