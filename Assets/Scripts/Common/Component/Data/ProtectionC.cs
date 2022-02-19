using Game.Common;

namespace Game.Game
{
    public struct ProtectionC
    {
        public float Protection;

        public bool HaveAnyProtection => Protection > 0;
    }
}