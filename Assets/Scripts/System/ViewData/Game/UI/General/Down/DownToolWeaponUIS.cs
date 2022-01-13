using Game.Common;
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


            //Button<TextUIC>(TWTypes.Pick).Text = EntInventorToolWeapon.ToolWeapons<AmountC>(TWTypes.Pick, LevelTypes.Second, EntWhoseMove.CurPlayerI).Amount.ToString();
            //Button<TextUIC>(TWTypes.Sword).Text = EntInventorToolWeapon.ToolWeapons<AmountC>(TWTypes.Sword, LevelTypes.Second, EntWhoseMove.CurPlayerI).Amount.ToString();
            //Button<TextUIC>(TWTypes.Shield).Text = EntInventorToolWeapon.ToolWeapons<AmountC>(TWTypes.Shield, TwGiveTakeC.Level(TWTypes.Shield), EntWhoseMove.CurPlayerI).Amount.ToString();
        }
    }
}
