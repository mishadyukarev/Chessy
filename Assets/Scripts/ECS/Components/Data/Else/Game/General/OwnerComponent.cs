using Photon.Realtime;

internal struct OwnerComponent
{
    internal Player Owner { get; set; }

    internal bool HaveOwner => Owner != default;
    internal bool IsMasterClient => Owner.IsMasterClient;
    internal int ActorNumber => Owner.ActorNumber;
    internal bool IsMine => Owner.IsLocal;

    internal void SetOwner(Player newOwner) => Owner = newOwner;
    internal void ResetOwner() => Owner = default;
    internal bool IsHim(Player player) => ActorNumber == player.ActorNumber;
}
