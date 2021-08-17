using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Down;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.Sync.DownZone
{
    internal sealed class GiveThingUISystem : IEcsRunSystem
    {
        private EcsFilter<GiveThingZoneViewUIComp> _giveThingUIFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;

        public void Run()
        {
            ref var giveThingZoneViewUIComp = ref _giveThingUIFilter.Get1(0);

            if (_selectorFilter.Get1(0).ToolWeaponTypeForGiveTake == ToolWeaponTypes.Pick)
            {
                giveThingZoneViewUIComp.Enable_ButtonImage(ToolWeaponTypes.Pick);
            }
            else
            {
                giveThingZoneViewUIComp.Disable_ButtonImage(ToolWeaponTypes.Pick);
            }

            if (_selectorFilter.Get1(0).ToolWeaponTypeForGiveTake == ToolWeaponTypes.Sword)
            {
                giveThingZoneViewUIComp.Enable_ButtonImage(ToolWeaponTypes.Sword);
            }
            else
            {
                giveThingZoneViewUIComp.Disable_ButtonImage(ToolWeaponTypes.Sword);
            }

            if (_selectorFilter.Get1(0).ToolWeaponTypeForGiveTake == ToolWeaponTypes.Crossbow)
            {
                giveThingZoneViewUIComp.Enable_ButtonImage(ToolWeaponTypes.Crossbow);
            }
            else
            {
                giveThingZoneViewUIComp.Disable_ButtonImage(ToolWeaponTypes.Crossbow);
            }
        }
    }
}
