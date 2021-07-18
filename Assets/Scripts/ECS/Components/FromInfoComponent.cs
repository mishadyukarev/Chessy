using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Components
{
    public struct FromInfoComponent
    {
        private PhotonMessageInfo _fromInfo;

        internal PhotonMessageInfo InfoFrom => _fromInfo;


        internal void StartFill() => _fromInfo = default;
        internal void SetFromInfo(PhotonMessageInfo fromInfo) => _fromInfo = fromInfo;
    }
}
