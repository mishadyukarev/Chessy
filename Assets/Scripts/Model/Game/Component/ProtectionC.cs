namespace Chessy.Game
{
    public struct ProtectionC
    {
        public float Protection { get; internal set; }

        public bool HaveAnyProtection => Protection > 0;
    }
}