using Chessy.Common;

namespace Chessy.Game
{
    public struct ProtectionC
    {
        public float Protection;

        public bool HaveAnyProtection => Protection > 0;
    }
}