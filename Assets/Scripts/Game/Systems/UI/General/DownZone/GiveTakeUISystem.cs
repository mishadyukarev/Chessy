using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class GiveTakeUISystem : IEcsRunSystem
    {
        private EcsFilter<GiveTakeViewUICom> _giveThingUIFilter = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;
        private EcsFilter<InventorTWCom> _inventToolsFilter = default;



        public void Run()
        {
            ref var inventToolsCom = ref _inventToolsFilter.Get1(0);
            ref var giveTakeViewCom = ref _giveThingUIFilter.Get1(0);
            ref var selCom = ref _selectorFilter.Get1(0);


            giveTakeViewCom.SetView_ButtonImage(ToolWeaponTypes.Pick, false);
            giveTakeViewCom.SetView_ButtonImage(ToolWeaponTypes.Sword, false);
            giveTakeViewCom.SetView_ButtonImage(ToolWeaponTypes.Shield, false);

            if (selCom.IsCellClickType(CellClickTypes.GiveTakeTW))
                if (selCom.TWTypeForGive != default)
                {
                    giveTakeViewCom.SetView_ButtonImage(selCom.TWTypeForGive, true);
                    giveTakeViewCom.SetImage(selCom.TWTypeForGive, selCom.LevelTWType);
                }

            giveTakeViewCom.SetText(ToolWeaponTypes.Pick, inventToolsCom.GetAmountTools(WhoseMoveCom.CurPlayer, ToolWeaponTypes.Pick, selCom.LevelTWType).ToString());
            giveTakeViewCom.SetText(ToolWeaponTypes.Sword, inventToolsCom.GetAmountTools(WhoseMoveCom.CurPlayer, ToolWeaponTypes.Sword, selCom.LevelTWType).ToString());
            giveTakeViewCom.SetText(ToolWeaponTypes.Shield, inventToolsCom.GetAmountTools(WhoseMoveCom.CurPlayer, ToolWeaponTypes.Shield, selCom.LevelTWType).ToString());
        }
    } 
}
