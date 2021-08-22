using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Center;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone
{
    internal sealed class KingZoneUISys : IEcsRunSystem
    {
        private EcsFilter<KingZoneViewUIComp> _kingZoneFilter = default;
        private EcsFilter<InventorUnitsComponent> _inventUnitsFilter = default;

        public void Run()
        {
            if (_inventUnitsFilter.Get1(0).HaveUnitInInventor(UnitTypes.King, PhotonNetwork.IsMasterClient))
            {
                _kingZoneFilter.Get1(0).EnableZone();     
            }
            else
            {
                _kingZoneFilter.Get1(0).DisableZone();
            }
        }
    }
}
