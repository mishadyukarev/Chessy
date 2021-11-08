namespace Chessy.Game
{
    public readonly struct FriendZoneDataUIC
    {
        public static bool IsActiveFriendZone { get; set; }

        public FriendZoneDataUIC(bool isActiveFriendZone) => IsActiveFriendZone = isActiveFriendZone;
    }
}
