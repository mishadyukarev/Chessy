using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Center;
using Assets.Scripts.ECS.Components.Data.Else.Common;
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
            ref var kingZoneViewCom = ref _kingZoneFilter.Get1(0);

            if (_inventUnitsFilter.Get1(0).HaveUnitInInventor(UnitTypes.King, PhotonNetwork.IsMasterClient))
            {
                kingZoneViewCom.SetTextKingBut(LanguageComComp.GetText(GameLanguageTypes.SetKing));
                kingZoneViewCom.EnableZone();
            }
            else
            {
                kingZoneViewCom.DisableZone();
            }
        }
    }
}
