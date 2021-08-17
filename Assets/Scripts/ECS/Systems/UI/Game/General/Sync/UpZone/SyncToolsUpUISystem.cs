using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone
{
    internal sealed class SyncToolsUpUISystem : IEcsRunSystem
    {
        private EcsFilter<ToolsViewUIComp> _toolUpUIFilter = default;
        private EcsFilter<InventorToolsComp> _inventToolsFilter = default;
        private EcsFilter<InventorWeaponsComp> _inventrorWeaponsFilter = default;

        public void Run()
        {
            ref var toolsViewUICom = ref _toolUpUIFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolsFilter.Get1(0);
            ref var inventorWeaponsComp = ref _inventrorWeaponsFilter.Get1(0);

            toolsViewUICom.TextAmountPicks = inventToolsCom.GetAmountTools(PhotonNetwork.IsMasterClient, ToolTypes.Pick).ToString();
            toolsViewUICom.TextAmountSwords = inventorWeaponsComp.GetAmountWeapons(PhotonNetwork.IsMasterClient, WeaponTypes.Sword).ToString();
        }
    }
}
