using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Center;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone
{
    internal sealed class KingZoneUISys : IEcsRunSystem
    {
        private EcsFilter<KingZoneViewUIComp> _kingZoneFilter = default;
        private EcsFilter<InventorUnitsComponent> _invUnitFil = default;

        private EcsFilter<WhoseMoveCom> _whoseMoveFilt = default;

        public void Run()
        {
            ref var kingZoneViewCom = ref _kingZoneFilter.Get1(0);


            var isMaster = false;

            if (PhotonNetwork.OfflineMode) isMaster = WhoseMoveCom.IsMainMove;

            else isMaster = PhotonNetwork.IsMasterClient;


            if (_invUnitFil.Get1(0).HaveUnitInInv(UnitTypes.King, isMaster))
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
