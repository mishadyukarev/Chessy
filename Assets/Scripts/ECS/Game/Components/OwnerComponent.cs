using Photon.Realtime;
using System;

internal struct OwnerComponent
{
    private Player _owner;

    internal Player Owner => _owner;
    internal bool HaveOwner => _owner != default;
    internal int ActorNumber => _owner.ActorNumber;
    internal bool IsMine => _owner.IsLocal;
    internal bool IsMasterClient => _owner.IsMasterClient;


    internal void StartFill() => _owner = default;
    internal bool IsHim(Player player) => player.ActorNumber == _owner.ActorNumber;
    internal void SetOwner(Player owner) => _owner = owner;
}
