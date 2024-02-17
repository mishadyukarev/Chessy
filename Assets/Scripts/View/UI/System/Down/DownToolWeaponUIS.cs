using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;
using UnityEngine;
namespace Chessy.Model
{
    sealed class DownToolWeaponUIS : SystemUIAbstract
    {
        readonly DownToolWeaponUIE _twE;

        internal DownToolWeaponUIS(in DownToolWeaponUIE twE, in EntitiesModel ents) : base(ents)
        {
            _twE = twE;
        }

        internal override void Sync()
        {
            var needActiveZone = false;


            if (!aboutGameC.LessonType.HaveLesson() || aboutGameC.LessonType >= LessonTypes.GiveTakePickPawn)
            {
                needActiveZone = true;

            }


            _twE.ParentGOC.TrySetActive(needActiveZone);


            if (needActiveZone)
            {
                var needStaff = false;
                var needBowCrossbow = false;
                var needAxe = false;
                var needPick = false;
                var needShield = false;
                var needSword = false;


                if (aboutGameC.LessonType.HaveLesson())
                {
                    needPick = true;

                    if (aboutGameC.LessonType >= LessonTypes.GiveIronAxe)
                    {
                        needAxe = true;

                        //if (_aboutGameC.LessonType >= LessonTypes.GiveStaff)
                        //{
                        //    needStaff = true;

                        //    if (_aboutGameC.LessonType >= LessonTypes.GiveBowCrossbow)
                        //    {
                        //        needBowCrossbow = true;

                        //        if (_aboutGameC.LessonType >= LessonTypes.GiveShield)
                        //        {
                        //            needShield = true;

                        //            if (_aboutGameC.LessonType >= LessonTypes.GiveSword)
                        //            {
                        //                needSword = true;


                        //            }
                        //        }
                        //    }
                        //}
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

                _twE.ButtonC(ToolsWeaponsWarriorTypes.Staff).SetActiveParent(needStaff);
                _twE.ButtonC(ToolsWeaponsWarriorTypes.BowCrossbow).SetActiveParent(needBowCrossbow);
                _twE.ButtonC(ToolsWeaponsWarriorTypes.Axe).SetActiveParent(needAxe);
                _twE.ButtonC(ToolsWeaponsWarriorTypes.Pick).SetActiveParent(needPick);
                _twE.ButtonC(ToolsWeaponsWarriorTypes.Shield).SetActiveParent(needShield);
                _twE.ButtonC(ToolsWeaponsWarriorTypes.Sword).SetActiveParent(needSword);



                Color color;

                for (var twT = ToolsWeaponsWarriorTypes.None + 1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                {
                    for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                    {
                        _twE.LevelImageC(twT, levT).SetActive(false);
                    }

                    color = _twE.ImageC(twT).Image.color;
                    color.a = 0;
                    _twE.ImageC(twT).Image.color = color;
                }


                var tw_sel = selectedToolWeaponC.ToolWeaponT;
                var levTw_sel = selectedToolWeaponC.LevelT;

                color = _twE.ImageC(tw_sel).Image.color;
                color.a = 1;
                _twE.ImageC(tw_sel).Image.color = color;


                _twE.LevelImageC(tw_sel, levTw_sel).SetActive(true);


                for (var twT = ToolsWeaponsWarriorTypes.None + 1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                {
                    _twE.LevelImageC(twT, levTw_sel).SetActive(true);
                }

                var curPlayerI = aboutGameC.CurrentPlayerIType;

                _twE.TextC(ToolsWeaponsWarriorTypes.Pick).TextUI.text = ToolWeaponsInInventoryC(curPlayerI).ToolWeapons(ToolsWeaponsWarriorTypes.Pick, LevelTypes.First).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Sword).TextUI.text = ToolWeaponsInInventoryC(curPlayerI).ToolWeapons(ToolsWeaponsWarriorTypes.Sword, LevelTypes.Second).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Axe).TextUI.text = ToolWeaponsInInventoryC(curPlayerI).ToolWeapons(ToolsWeaponsWarriorTypes.Axe, LevelTypes.Second).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Shield).TextUI.text = ToolWeaponsInInventoryC(curPlayerI).ToolWeapons(ToolsWeaponsWarriorTypes.Shield, selectedToolWeaponC.LevelT).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.BowCrossbow).TextUI.text = ToolWeaponsInInventoryC(curPlayerI).ToolWeapons(ToolsWeaponsWarriorTypes.BowCrossbow, selectedToolWeaponC.LevelT).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Staff).TextUI.text = ToolWeaponsInInventoryC(curPlayerI).ToolWeapons(ToolsWeaponsWarriorTypes.Staff, LevelTypes.First).ToString();
            }
        }
    }
}
