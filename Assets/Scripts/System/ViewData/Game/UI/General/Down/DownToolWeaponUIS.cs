using static Game.Game.UIEntDownToolWeapon;

namespace Game.Game
{
    struct DownToolWeaponUIS : IEcsRunSystem
    {
        public void Run()
        {
            //Button<ButtonUIC>(TWTypes.Pick).SetActive(false);
            //Button<ButtonUIC>(TWTypes.Sword).SetActive(false);
            //Button<ButtonUIC>(TWTypes.Shield).SetActive(false);

            //if (ClickerObject<CellClickC>().Is(CellClickTypes.GiveTakeTW))
            //    if (TwGiveTakeC.TWTypeForGive != default)
            //    {
            //        UIEntityDownTW.SetView_ButtonImage(TwGiveTakeC.TWTypeForGive, true);
            //        UIEntityDownTW.SetImage(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive));


            //    }


            Button<TextUIC>(TWTypes.Pick).Text = InvTWC.Amount(TWTypes.Pick, LevelTypes.Second, WhoseMoveC.CurPlayerI).ToString();
            Button<TextUIC>(TWTypes.Sword).Text = InvTWC.Amount(TWTypes.Sword, LevelTypes.Second, WhoseMoveC.CurPlayerI).ToString();
            Button<TextUIC>(TWTypes.Shield).Text = InvTWC.Amount(TWTypes.Shield, TwGiveTakeC.Level(TWTypes.Shield), WhoseMoveC.CurPlayerI).ToString();
        }
    }
}
