using UnityEngine;

namespace Chessy.Game
{
    sealed class DownToolWeaponUIS : SystemModelGameAbs, IEcsRunSystem
    {
        readonly DownToolWeaponUIE _twE;

        internal DownToolWeaponUIS(in DownToolWeaponUIE twE, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
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


            var tw_sel = eMGame.SelectedE.ToolWeaponC.ToolWeaponT;
            var levTw_sel = eMGame.SelectedE.ToolWeaponC.LevelT;

            color = _twE.ImageC(tw_sel).Image.color;
            color.a = 1;
            _twE.ImageC(tw_sel).Image.color = color;


            _twE.LevelImageC(tw_sel, levTw_sel).SetActive(true);


            for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
            {
                _twE.LevelImageC(twT, levTw_sel).SetActive(true);
            }

            var curPlayerI = eMGame.CurPlayerITC.Player;

            _twE.TextC(ToolWeaponTypes.Pick).TextUI.text = eMGame.PlayerInfoE(curPlayerI).LevelE(LevelTypes.First).ToolWeapons(ToolWeaponTypes.Pick).ToString();
            _twE.TextC(ToolWeaponTypes.Sword).TextUI.text = eMGame.PlayerInfoE(curPlayerI).LevelE(LevelTypes.Second).ToolWeapons(ToolWeaponTypes.Sword).ToString();
            _twE.TextC(ToolWeaponTypes.Axe).TextUI.text = eMGame.PlayerInfoE(curPlayerI).LevelE(LevelTypes.Second).ToolWeapons(ToolWeaponTypes.Axe).ToString();
            _twE.TextC(ToolWeaponTypes.Shield).TextUI.text = eMGame.PlayerInfoE(curPlayerI).LevelE(eMGame.SelectedE.ToolWeaponC.LevelT).ToolWeapons(ToolWeaponTypes.Shield).ToString();
            _twE.TextC(ToolWeaponTypes.BowCrossbow).TextUI.text = eMGame.PlayerInfoE(curPlayerI).LevelE(eMGame.SelectedE.ToolWeaponC.LevelT).ToolWeapons(ToolWeaponTypes.BowCrossbow).ToString();
            _twE.TextC(ToolWeaponTypes.Staff).TextUI.text = eMGame.PlayerInfoE(curPlayerI).LevelE(LevelTypes.First).ToolWeapons(ToolWeaponTypes.Staff).ToString();
        }
    }
}
