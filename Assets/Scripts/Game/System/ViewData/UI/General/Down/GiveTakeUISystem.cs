using Leopotam.Ecs;

namespace Game.Game
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

            TwGiveTakeUIC.SetText(TWTypes.Pick, InvTWC.Amount(TWTypes.Pick, LevelTypes.Second, WhoseMoveC.CurPlayerI).ToString());
            TwGiveTakeUIC.SetText(TWTypes.Sword, InvTWC.Amount(TWTypes.Sword, LevelTypes.Second, WhoseMoveC.CurPlayerI).ToString());
            TwGiveTakeUIC.SetText(TWTypes.Shield, InvTWC.Amount(TWTypes.Shield, TwGiveTakeC.Level(TWTypes.Shield), WhoseMoveC.CurPlayerI).ToString());
        }
    }
}
