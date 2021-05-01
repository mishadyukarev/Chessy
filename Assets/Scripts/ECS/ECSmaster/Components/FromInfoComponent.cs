using Photon.Pun;


internal struct FromInfoComponent
{
    private PhotonMessageInfo _fromInfo;

    internal PhotonMessageInfo FromInfo
    {
        get { return _fromInfo; }
        set { _fromInfo = value; }
    }
}
