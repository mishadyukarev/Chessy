using UnityEngine;

namespace Chessy.Game
{
    sealed class DownToolWeaponUIS : SystemAbstract, IEcsRunSystem
    {
        readonly DownToolWeaponUIE _twE;

        internal DownToolWeaponUIS(in DownToolWeaponUIE twE, in EntitiesModel ents) : base(ents)
        {
            _twE = twE;
        }

        public void Run()
        {
            Color color;

            for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
            {
                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    _twE .LevelImageC(twT, levT).SetActive(false);
                }

                color = _twE.ImageC(twT).Image.color;
                color.a = 0;
                _twE.ImageC(twT).Image.color = color;
            }


            var tw_sel = E.SelectedTWE.ToolWeaponTC.ToolWeapon;
            var levTw_sel = E.SelectedTWE.LevelTC.Level;

            color = _twE.ImageC(tw_sel).Image.color;
            color.a = 1;
            _twE.ImageC(tw_sel).Image.color = color;


            _twE.LevelImageC(tw_sel, levTw_sel).SetActive(true);


            for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
            {
                _twE.LevelImageC(twT, levTw_sel).SetActive(true);
            }

            var curPlayerI = E.CurPlayerITC.Player;

            _twE.TextC(ToolWeaponTypes.Pick).TextUI.text = E.PlayerInfoE(curPlayerI).LevelE(LevelTypes.First).ToolWeapons(ToolWeaponTypes.Pick).Amount.ToString();
            _twE.TextC(ToolWeaponTypes.Sword).TextUI.text = E.PlayerInfoE(curPlayerI).LevelE(LevelTypes.Second).ToolWeapons(ToolWeaponTypes.Sword).Amount.ToString();
            _twE.TextC(ToolWeaponTypes.Axe).TextUI.text = E.PlayerInfoE(curPlayerI).LevelE(LevelTypes.Second).ToolWeapons(ToolWeaponTypes.Axe).Amount.ToString();
            _twE.TextC(ToolWeaponTypes.Shield).TextUI.text = E.PlayerInfoE(curPlayerI).LevelE(E.SelectedTWE.LevelTC.Level).ToolWeapons(ToolWeaponTypes.Shield).Amount.ToString();
            _twE.TextC(ToolWeaponTypes.BowCrossbow).TextUI.text = E.PlayerInfoE(curPlayerI).LevelE(E.SelectedTWE.LevelTC.Level).ToolWeapons(ToolWeaponTypes.BowCrossbow).Amount.ToString();
            _twE.TextC(ToolWeaponTypes.Staff).TextUI.text = E.PlayerInfoE(curPlayerI).LevelE(LevelTypes.First).ToolWeapons(ToolWeaponTypes.Staff).Amount.ToString();
        }
    }
}
