using Photon.Pun;
using Photon.Realtime;


internal struct FromInfoComponent
{
    private PhotonMessageInfo _fromInfo;

    internal PhotonMessageInfo FromInfo
    {
        get { return _fromInfo; }
        set { _fromInfo = value; }
    }
}
