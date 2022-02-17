using UnityEngine;
using static Game.Game.DownToolWeaponUIEs;

namespace Game.Game
{
    sealed class DownToolWeaponUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownToolWeaponUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            Color color;

            for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
            {
                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    Image(twT, levT).SetActive(false);
                }

                color = Button<ImageUIC>(twT).Color;
                color.a = 0;
                Button<ImageUIC>(twT).Color = color;
            }


            var tw_sel = Es.SelectedTWTC.ToolWeapon;
            var levTw_sel = Es.SelectedTWLevelTC.Level;

            color = Button<ImageUIC>(tw_sel).Color;
            color.a = 1;
            Button<ImageUIC>(tw_sel).Color = color;


            Image(tw_sel, levTw_sel).SetActive(true);


            for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
            {
                Image(twT, levTw_sel).SetActive(true);
            }

            var curPlayerI = Es.CurPlayerI.Player;

            Button<TextUIC>(ToolWeaponTypes.Pick).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Pick, LevelTypes.Second, curPlayerI).ToolWeaponsC.ToolWeapons.ToString();
            Button<TextUIC>(ToolWeaponTypes.Sword).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Sword, LevelTypes.Second, curPlayerI).ToolWeaponsC.ToolWeapons.ToString();
            Button<TextUIC>(ToolWeaponTypes.Axe).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Axe, LevelTypes.Second, curPlayerI).ToolWeaponsC.ToolWeapons.ToString();
            Button<TextUIC>(ToolWeaponTypes.Shield).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.Shield, Es.SelectedTWLevelTC.Level, curPlayerI).ToolWeaponsC.ToolWeapons.ToString();
            Button<TextUIC>(ToolWeaponTypes.BowCrossbow).Text = Es.InventorToolWeaponEs.ToolWeapons(ToolWeaponTypes.BowCrossbow, Es.SelectedTWLevelTC.Level, curPlayerI).ToolWeaponsC.ToolWeapons.ToString();
        }
    }
}
