using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Components
{
    public struct FromInfoComponent
    {
        private PhotonMessageInfo _photonMessageInfo;

        internal PhotonMessageInfo Info
        {
            get => _photonMessageInfo;
            set => _photonMessageInfo = value;
        }

        internal void StartFill()
        {

        }
    }
}
