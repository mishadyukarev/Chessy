﻿using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game
{
    sealed class DownToolWeaponUIS : SystemUIAbstract
    {
        readonly DownToolWeaponUIE _twE;

        internal DownToolWeaponUIS(in DownToolWeaponUIE twE, in EntitiesModelGame ents) : base(ents)
        {
            _twE = twE;
        }

        internal override void Sync()
        {
            var needActiveZone = false;


            if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= Enum.LessonTypes.ClickPick)
            {
                needActiveZone = true;

            }


            _twE.ParentGOC.SetActive(needActiveZone);


            if (needActiveZone)
            {
                var needStaff = false;
                var needBowCrossbow = false;
                var needAxe = false;
                var needPick = false;
                var needShield = false;
                var needSword = false;


                if (e.LessonTC.HaveLesson)
                {
                    needPick = true;
                    if (e.LessonT >= Enum.LessonTypes.ShieldAndToolWeaponsInfo)
                    {
                        needStaff = true;
                        needBowCrossbow = true;
                        needAxe = true;
                        needShield = true;
                        needSword = true;
                    }
                }
                else
                {
                    needStaff = true;
                    needBowCrossbow = true;
                    needAxe = true;
                    needPick = true;
                    needShield = true;
                    needSword = true;
                }

                _twE.ButtonC(ToolWeaponTypes.Staff).SetActiveParent(needStaff);
                _twE.ButtonC(ToolWeaponTypes.BowCrossbow).SetActiveParent(needBowCrossbow);
                _twE.ButtonC(ToolWeaponTypes.Axe).SetActiveParent(needAxe);
                _twE.ButtonC(ToolWeaponTypes.Pick).SetActiveParent(needPick);
                _twE.ButtonC(ToolWeaponTypes.Shield).SetActiveParent(needShield);
                _twE.ButtonC(ToolWeaponTypes.Sword).SetActiveParent(needSword);



                Color color;

                for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
                {
                    for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                    {
                        _twE.LevelImageC(twT, levT).SetActive(false);
                    }

                    color = _twE.ImageC(twT).Image.color;
                    color.a = 0;
                    _twE.ImageC(twT).Image.color = color;
                }


                var tw_sel = e.SelectedE.ToolWeaponC.ToolWeaponT;
                var levTw_sel = e.SelectedE.ToolWeaponC.LevelT;

                color = _twE.ImageC(tw_sel).Image.color;
                color.a = 1;
                _twE.ImageC(tw_sel).Image.color = color;


                _twE.LevelImageC(tw_sel, levTw_sel).SetActive(true);


                for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
                {
                    _twE.LevelImageC(twT, levTw_sel).SetActive(true);
                }

                var curPlayerI = e.CurPlayerITC.PlayerT;

                _twE.TextC(ToolWeaponTypes.Pick).TextUI.text = e.PlayerInfoE(curPlayerI).LevelE(LevelTypes.First).ToolWeapons(ToolWeaponTypes.Pick).ToString();
                _twE.TextC(ToolWeaponTypes.Sword).TextUI.text = e.PlayerInfoE(curPlayerI).LevelE(LevelTypes.Second).ToolWeapons(ToolWeaponTypes.Sword).ToString();
                _twE.TextC(ToolWeaponTypes.Axe).TextUI.text = e.PlayerInfoE(curPlayerI).LevelE(LevelTypes.Second).ToolWeapons(ToolWeaponTypes.Axe).ToString();
                _twE.TextC(ToolWeaponTypes.Shield).TextUI.text = e.PlayerInfoE(curPlayerI).LevelE(e.SelectedE.ToolWeaponC.LevelT).ToolWeapons(ToolWeaponTypes.Shield).ToString();
                _twE.TextC(ToolWeaponTypes.BowCrossbow).TextUI.text = e.PlayerInfoE(curPlayerI).LevelE(e.SelectedE.ToolWeaponC.LevelT).ToolWeapons(ToolWeaponTypes.BowCrossbow).ToString();
                _twE.TextC(ToolWeaponTypes.Staff).TextUI.text = e.PlayerInfoE(curPlayerI).LevelE(LevelTypes.First).ToolWeapons(ToolWeaponTypes.Staff).ToString();
            }
        }
    }
}
