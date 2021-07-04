﻿using Photon.Realtime;
using System;

internal struct OwnerComponent
{
    private Player _owner;

    internal Player Owner => _owner;
    internal bool HaveOwner => _owner != default;

    internal int ActorNumber => _owner.ActorNumber;

    internal bool IsMine => _owner.IsLocal;

    internal bool IsMasterClient => _owner.IsMasterClient;

    internal bool IsHim(Player player)
    {
        if (player == default) throw new Exception();
        return player.ActorNumber == _owner.ActorNumber;
    }

    internal void SetOwner(Player owner) => _owner = owner;

    internal void StartFill()
    {
        _owner = default;
    }
}