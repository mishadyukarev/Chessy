﻿using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Down;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.DownZone
{
    internal sealed class GiveTakeUISystem : IEcsRunSystem
    {
        private EcsFilter<GiveTakeZoneViewUIComp> _giveThingUIFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;

        public void Run()
        {
            ref var giveTakeZoneViewUIComp = ref _giveThingUIFilter.Get1(0);
            ref var selectorComp = ref _selectorFilter.Get1(0);


            giveTakeZoneViewUIComp.Disable_ButtonImage(ToolWeaponTypes.Axe);
            giveTakeZoneViewUIComp.Disable_ButtonImage(ToolWeaponTypes.Pick);
            giveTakeZoneViewUIComp.Disable_ButtonImage(ToolWeaponTypes.Sword);
            giveTakeZoneViewUIComp.Disable_ButtonImage(ToolWeaponTypes.Crossbow);

            giveTakeZoneViewUIComp.Enable_ButtonImage(selectorComp.ToolWeaponTypeForGiveTake);



            if (selectorComp.IsCellClickType(CellClickTypes.GiveTakeToolWeapon))
            {
                giveTakeZoneViewUIComp.SetColorToGiveTake_Button(Color.yellow);
                giveTakeZoneViewUIComp.SetColorToSwap_Button(Color.white);
            }
            else
            {
                giveTakeZoneViewUIComp.SetColorToGiveTake_Button(Color.white);

                if (selectorComp.IsCellClickType(CellClickTypes.SwapToolWeapon))
                {
                    giveTakeZoneViewUIComp.SetColorToSwap_Button(Color.green);
                }

                else
                {
                    giveTakeZoneViewUIComp.SetColorToSwap_Button(Color.white);
                }
            }    
        }
    }
}
