﻿using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Down;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone
{
    internal sealed class ToolsDownUISys : IEcsRunSystem
    {
        private EcsFilter<GiveTakeZoneViewUIComp> _giveTakeZoneViewUIFilter = default;
        private EcsFilter<InventorTWCom> _inventToolsFilter = default;

        public void Run()
        {
            ref var giveTakeZoneViewUICom = ref _giveTakeZoneViewUIFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolsFilter.Get1(0);

            //giveTakeZoneViewUICom.SetText(ToolWeaponTypes.Pick, inventToolsCom.GetAmountTools(PhotonNetwork.IsMasterClient, ToolTypes.Pick).ToString());
            //giveTakeZoneViewUICom.SetText(ToolWeaponTypes.Sword, inventorWeaponsComp.GetAmountWeapons(PhotonNetwork.IsMasterClient, WeaponTypes.Sword).ToString());
            //giveTakeZoneViewUICom.SetText(ToolWeaponTypes.Crossbow, inventorWeaponsComp.GetAmountWeapons(PhotonNetwork.IsMasterClient, WeaponTypes.Crossbow).ToString());
        }
    }
}
