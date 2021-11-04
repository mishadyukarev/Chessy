using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class GiveTakeUISystem : IEcsRunSystem
    {
        public void Run()
        {
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Pick, false);
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Sword, false);
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Shield, false);

            if (SelectorC.Is(CellClickTypes.GiveTakeTW))
                if (SelectorC.TWTypeForGive != default)
                {
                    GiveTakeViewUIC.SetView_ButtonImage(SelectorC.TWTypeForGive, true);
                    GiveTakeViewUIC.SetImage(SelectorC.TWTypeForGive, GiveTakeDataUIC.Level(SelectorC.TWTypeForGive));
                }

            GiveTakeViewUIC.SetText(ToolWeaponTypes.Pick, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Pick, LevelTWTypes.Iron).ToString());
            GiveTakeViewUIC.SetText(ToolWeaponTypes.Sword, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Sword, LevelTWTypes.Iron).ToString());
            GiveTakeViewUIC.SetText(ToolWeaponTypes.Shield, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Shield, GiveTakeDataUIC.Level(ToolWeaponTypes.Shield)).ToString());
        }
    }
}
