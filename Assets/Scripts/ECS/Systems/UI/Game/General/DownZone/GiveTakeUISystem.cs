using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Down;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.DownZone
{
    internal sealed class GiveTakeUISystem : IEcsRunSystem
    {
        private EcsFilter<GiveTakeZoneViewUIComp> _giveThingUIFilter = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;

        public void Run()
        {
            ref var giveTakeViewCom = ref _giveThingUIFilter.Get1(0);
            ref var selectorComp = ref _selectorFilter.Get1(0);


            giveTakeViewCom.Disable_ButtonImage(ToolWeaponTypes.Axe);
            giveTakeViewCom.Disable_ButtonImage(ToolWeaponTypes.Pick);
            giveTakeViewCom.Disable_ButtonImage(ToolWeaponTypes.Sword);
            giveTakeViewCom.Disable_ButtonImage(ToolWeaponTypes.Crossbow);

            giveTakeViewCom.Enable_ButtonImage(selectorComp.ToolWeaponTypeForGiveTake);


            giveTakeViewCom.SetTextToGiveTake_Button(LanguageComCom.GetText(GameLanguageTypes.GiveTake));


            if (selectorComp.IsCellClickType(CellClickTypes.GiveTakeTW))
            {
                giveTakeViewCom.SetColorToGiveTake_Button(Color.yellow);
            }
            else
            {
                giveTakeViewCom.SetColorToGiveTake_Button(Color.white);
            }
        }
    }
}
