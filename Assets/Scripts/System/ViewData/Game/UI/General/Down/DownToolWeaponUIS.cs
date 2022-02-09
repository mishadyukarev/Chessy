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

            color = Button<ImageUIC>(ToolWeaponTypes.BowCrossbow).Color;
            color.a = 0;
            Button<ImageUIC>(ToolWeaponTypes.BowCrossbow).Color = color;


            var tw_sel = Es.SelectedToolWeaponE.ToolWeaponTC.ToolWeapon;
            var levTw_sel = Es.SelectedToolWeaponE.LevelTC.Level;

            color = Button<ImageUIC>(tw_sel).Color;
            color.a = 1;
            Button<ImageUIC>(tw_sel).Color = color;


            Image(tw_sel, levTw_sel).SetActive(true);

            if (levTw_sel == LevelTypes.First)
            {
                Image(tw_sel, LevelTypes.Second).SetActive(false);
            }
            else
            {
                Image(tw_sel, LevelTypes.First).SetActive(false);
            }

            Button<TextUIC>(ToolWeaponTypes.Pick).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Pick, LevelTypes.Second, Es.WhoseMoveE.CurPlayerI).ToolWeapons.Amount.ToString();
            Button<TextUIC>(ToolWeaponTypes.Sword).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Sword, LevelTypes.Second, Es.WhoseMoveE.CurPlayerI).ToolWeapons.Amount.ToString();
            Button<TextUIC>(ToolWeaponTypes.Shield).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Shield, Es.SelectedToolWeaponE.LevelTC.Level, Es.WhoseMoveE.CurPlayerI).ToolWeapons.Amount.ToString();
            Button<TextUIC>(ToolWeaponTypes.BowCrossbow).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.BowCrossbow, Es.SelectedToolWeaponE.LevelTC.Level, Es.WhoseMoveE.CurPlayerI).ToolWeapons.Amount.ToString();
        }
    }
}
