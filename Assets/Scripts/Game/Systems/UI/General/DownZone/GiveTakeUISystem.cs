using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class GiveTakeUISystem : IEcsRunSystem
    {
        public void Run()
        {
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Pick, false);
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Sword, false);
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Shield, false);

            if (SelectorC.IsCellClickType(CellClickTypes.GiveTakeTW))
                if (SelectorC.TWTypeForGive != default)
                {
                    GiveTakeViewUIC.SetView_ButtonImage(SelectorC.TWTypeForGive, true);
                    GiveTakeViewUIC.SetImage(SelectorC.TWTypeForGive, SelectorC.LevelTWType);
                }

            GiveTakeViewUIC.SetText(ToolWeaponTypes.Pick, InventorTWCom.GetAmountTools(WhoseMoveC.CurPlayer, ToolWeaponTypes.Pick, SelectorC.LevelTWType).ToString());
            GiveTakeViewUIC.SetText(ToolWeaponTypes.Sword, InventorTWCom.GetAmountTools(WhoseMoveC.CurPlayer, ToolWeaponTypes.Sword, SelectorC.LevelTWType).ToString());
            GiveTakeViewUIC.SetText(ToolWeaponTypes.Shield, InventorTWCom.GetAmountTools(WhoseMoveC.CurPlayer, ToolWeaponTypes.Shield, SelectorC.LevelTWType).ToString());
        }
    } 
}
