using Photon.Realtime;

internal struct OwnerComponent
{
    internal Player Owner;
    private bool _haveOwner => Owner != default;

    internal int ActorNumber
    {
        get
        {
            if (_haveOwner) return Owner.ActorNumber;
            else return -1;
        }
    }

    internal bool IsMine
    {
        get
        {
            if (_haveOwner) return Owner.IsLocal;
            else return default;
        }
    }

    internal bool IsMasterClient
    {
        get
        {
            if (_haveOwner) return Owner.IsMasterClient;
            else return default;
        }
    }


    internal bool IsHim(Player player)
    {
        if (player == default) return default;
        return player.ActorNumber == Owner.ActorNumber;
    }
}
