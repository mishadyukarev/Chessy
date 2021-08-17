using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Center;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone
{
    internal sealed class KingZoneUISys : IEcsRunSystem
    {
        private EcsFilter<UnitsInGameInfoComponent> _unitsInGameInfoFilter = default;
        private EcsFilter<KingZoneViewUIComp> _kingZoneFilter = default;

        public void Run()
        {
            if (_unitsInGameInfoFilter.Get1(0).IsSettedKing(PhotonNetwork.IsMasterClient))
            {
                _kingZoneFilter.Get1(0).DisableZone();
            }
            else
            {
                _kingZoneFilter.Get1(0).EnableZone();
            }
        }
    }
}
