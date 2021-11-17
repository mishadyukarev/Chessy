using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class GiveTakeUISystem : IEcsRunSystem
    {
        public void Run()
        {
            TwGiveTakeUIC.SetView_ButtonImage(TWTypes.Pick, false);
            TwGiveTakeUIC.SetView_ButtonImage(TWTypes.Sword, false);
            TwGiveTakeUIC.SetView_ButtonImage(TWTypes.Shield, false);

            if (CellClickC.Is(CellClickTypes.GiveTakeTW))
                if (TwGiveTakeC.TWTypeForGive != default)
                {
                    TwGiveTakeUIC.SetView_ButtonImage(TwGiveTakeC.TWTypeForGive, true);
                    TwGiveTakeUIC.SetImage(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive));
                }

            TwGiveTakeUIC.SetText(TWTypes.Pick, InvTWC.AmountToolWeap(WhoseMoveC.CurPlayerI, TWTypes.Pick, LevelTypes.Second).ToString());
            TwGiveTakeUIC.SetText(TWTypes.Sword, InvTWC.AmountToolWeap(WhoseMoveC.CurPlayerI, TWTypes.Sword, LevelTypes.Second).ToString());
            TwGiveTakeUIC.SetText(TWTypes.Shield, InvTWC.AmountToolWeap(WhoseMoveC.CurPlayerI, TWTypes.Shield, TwGiveTakeC.Level(TWTypes.Shield)).ToString());
        }
    }
}
