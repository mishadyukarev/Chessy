namespace Game.Game
{
    public struct ShieldEffectC
    {
        public int Protection;
        public bool IsDestroyed => Protection <= 0;
    }
}