using static Game.Game.DownToolWeaponUIEs;

namespace Game.Game
{
    sealed class DownToolWeaponUIS : SystemViewAbstract, IEcsRunSystem
    {
        public DownToolWeaponUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var color = Button<ImageUIC>(ToolWeaponTypes.Pick).Color;
            color.a = 0;
            Button<ImageUIC>(ToolWeaponTypes.Pick).Color = color;

            color = Button<ImageUIC>(ToolWeaponTypes.Sword).Color;
            color.a = 0;
            Button<ImageUIC>(ToolWeaponTypes.Sword).Color = color;

            color = Button<ImageUIC>(ToolWeaponTypes.Shield).Color;
            color.a = 0;
            Button<ImageUIC>(ToolWeaponTypes.Shield).Color = color;


            var tw_sel = Es.SelectedToolWeaponE.ToolWeaponTC.ToolWeapon;
            var levTw_sel = Es.SelectedToolWeaponE.LevelTC.Level;

            color = Button<ImageUIC>(tw_sel).Color;
            color.a = 1;
            Button<ImageUIC>(tw_sel).Color = color;


            Image<ImageUIC>(tw_sel, levTw_sel).SetActive(true);

            if (levTw_sel == LevelTypes.First)
            {
                Image<ImageUIC>(tw_sel, LevelTypes.Second).SetActive(false);
            }
            else
            {
                Image<ImageUIC>(tw_sel, LevelTypes.First).SetActive(false);
            }

            Button<TextUIC>(ToolWeaponTypes.Pick).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Pick, LevelTypes.Second, Es.WhoseMove.CurPlayerI).ToolWeapons.Amount.ToString();
            Button<TextUIC>(ToolWeaponTypes.Sword).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Sword, LevelTypes.Second, Es.WhoseMove.CurPlayerI).ToolWeapons.Amount.ToString();
            Button<TextUIC>(ToolWeaponTypes.Shield).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Shield, Es.SelectedToolWeaponE.LevelTC.Level, Es.WhoseMove.CurPlayerI).ToolWeapons.Amount.ToString();
        }
    }
}
