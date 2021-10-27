namespace Scripts.Game
{
    internal readonly struct FriendZoneDataUIC
    {
        internal static bool IsActiveFriendZone { get; set; }

        internal FriendZoneDataUIC(bool isActiveFriendZone) => IsActiveFriendZone = isActiveFriendZone;
    }
}
