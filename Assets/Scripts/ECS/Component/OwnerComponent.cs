using Photon.Realtime;

internal struct OwnerComponent
{
    internal Player Owner { get; set; }

    internal bool HaveOwner => Owner != default;
    internal bool IsMasterClient => Owner.IsMasterClient;
    internal int ActorNumber => Owner.ActorNumber;
    internal bool IsMine => Owner.IsLocal;

    internal bool IsHim(Player player) => ActorNumber == player.ActorNumber;
    internal void ResetOwner() => Owner = default;
}
