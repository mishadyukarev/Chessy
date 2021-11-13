namespace Chessy.Game
{
    public readonly struct FriendC
    {
        public static bool IsActiveFriendZone { get; set; }

        public FriendC(bool isActiveFriendZone) => IsActiveFriendZone = isActiveFriendZone;
    }
}
