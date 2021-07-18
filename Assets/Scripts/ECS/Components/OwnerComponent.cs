using Photon.Realtime;

internal struct OwnerComponent
{
    private Player _owner;

    internal Player Owner => _owner; //Need Redo

    internal bool HaveOwner => _owner != default;
    internal int ActorNumber => _owner.ActorNumber;
    internal bool IsMine => _owner.IsLocal;
    internal bool IsMasterClient => _owner.IsMasterClient;


    internal void StartFill(Player owner = default) => _owner = owner;
    internal bool IsHim(Player player) => player.ActorNumber == _owner.ActorNumber;
    internal void SetOwner(Player owner) => _owner = owner;
    internal void ResetOwner() => _owner = default;
}
