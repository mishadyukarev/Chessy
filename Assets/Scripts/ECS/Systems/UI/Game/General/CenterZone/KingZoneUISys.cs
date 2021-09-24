using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Center;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone
{
    internal sealed class KingZoneUISys : IEcsRunSystem
    {
        private EcsFilter<KingZoneViewUIComp> _kingZoneFilter = default;
        private EcsFilter<InventorUnitsComponent> _invUnitFil = default;

        public void Run()
        {
            ref var kingZoneViewCom = ref _kingZoneFilter.Get1(0);


            if (_invUnitFil.Get1(0).HaveUnitInInv(WhoseMoveCom.CurPlayer, UnitTypes.King))
            {
                kingZoneViewCom.SetTextKingBut(LanguageComCom.GetText(GameLanguageTypes.SetKing));
                kingZoneViewCom.EnableZone();
            }
            else
            {
                kingZoneViewCom.DisableZone();
            }
        }
    }
}
