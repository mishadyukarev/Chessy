using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class GiveTakeUISystem : IEcsRunSystem
    {
        public void Run()
        {
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Pick, false);
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Sword, false);
            GiveTakeViewUIC.SetView_ButtonImage(ToolWeaponTypes.Shield, false);

            if (CellClickC.Is(CellClickTypes.GiveTakeTW))
                if (TwGiveTakeC.TWTypeForGive != default)
                {
                    GiveTakeViewUIC.SetView_ButtonImage(TwGiveTakeC.TWTypeForGive, true);
                    GiveTakeViewUIC.SetImage(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive));
                }

            GiveTakeViewUIC.SetText(ToolWeaponTypes.Pick, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Pick, LevelTWTypes.Iron).ToString());
            GiveTakeViewUIC.SetText(ToolWeaponTypes.Sword, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Sword, LevelTWTypes.Iron).ToString());
            GiveTakeViewUIC.SetText(ToolWeaponTypes.Shield, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Shield, TwGiveTakeC.Level(ToolWeaponTypes.Shield)).ToString());
        }
    }
}
