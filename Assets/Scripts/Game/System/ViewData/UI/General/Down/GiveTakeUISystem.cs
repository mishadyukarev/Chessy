using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class GiveTakeUISystem : IEcsRunSystem
    {
        public void Run()
        {
            TwGiveTakeUIC.SetView_ButtonImage(ToolWeaponTypes.Pick, false);
            TwGiveTakeUIC.SetView_ButtonImage(ToolWeaponTypes.Sword, false);
            TwGiveTakeUIC.SetView_ButtonImage(ToolWeaponTypes.Shield, false);

            if (CellClickC.Is(CellClickTypes.GiveTakeTW))
                if (TwGiveTakeC.TWTypeForGive != default)
                {
                    TwGiveTakeUIC.SetView_ButtonImage(TwGiveTakeC.TWTypeForGive, true);
                    TwGiveTakeUIC.SetImage(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive));
                }

            TwGiveTakeUIC.SetText(ToolWeaponTypes.Pick, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Pick, LevelTWTypes.Iron).ToString());
            TwGiveTakeUIC.SetText(ToolWeaponTypes.Sword, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Sword, LevelTWTypes.Iron).ToString());
            TwGiveTakeUIC.SetText(ToolWeaponTypes.Shield, InvToolWeapC.AmountToolWeap(WhoseMoveC.CurPlayerI, ToolWeaponTypes.Shield, TwGiveTakeC.Level(ToolWeaponTypes.Shield)).ToString());
        }
    }
}
