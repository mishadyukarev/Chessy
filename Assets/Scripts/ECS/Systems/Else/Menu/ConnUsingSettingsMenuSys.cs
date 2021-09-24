using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Else.Menu
{
    internal sealed class ConnUsingSettingsMenuSys : IEcsRunSystem
    {
        public void Run()
        {
            PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = Main.VERSION_PHOTON_GAME;
            PhotonNetwork.PhotonServerSettings.name = "Player " + UnityEngine.Random.Range(1, 999999);

            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
