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
            ref var selectorComp = ref _selectorFilter.Get1(0);


            giveTakeViewCom.SetView_ButtonImage(ToolWeaponTypes.Pick, false);
            giveTakeViewCom.SetView_ButtonImage(ToolWeaponTypes.Sword, false);
            giveTakeViewCom.SetView_ButtonImage(ToolWeaponTypes.Shield, false);

            if (selectorComp.TWTypeForGive != default)
            {
                giveTakeViewCom.SetView_ButtonImage(selectorComp.TWTypeForGive, true);
                giveTakeViewCom.SetImage(selectorComp.TWTypeForGive, selectorComp.LevelTWType);
            }

            giveTakeViewCom.SetText(ToolWeaponTypes.Pick, inventToolsCom.GetAmountTools(WhoseMoveCom.CurPlayer, ToolWeaponTypes.Pick).ToString());
            giveTakeViewCom.SetText(ToolWeaponTypes.Sword, inventToolsCom.GetAmountTools(WhoseMoveCom.CurPlayer, ToolWeaponTypes.Sword).ToString());
            giveTakeViewCom.SetText(ToolWeaponTypes.Shield, inventToolsCom.GetAmountTools(WhoseMoveCom.CurPlayer, ToolWeaponTypes.Shield).ToString());
        }
    } 
}
