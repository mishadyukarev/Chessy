using Chessy.Model.Entity;
using Chessy.Model.Enum;
using UnityEngine;
using Chessy.View.UI.Entity; namespace Chessy.Model
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


            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.GiveTakePickPawn)
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


                if (_e.LessonT.HaveLesson())
                {
                    needPick = true;

                    if (_e.LessonT >= LessonTypes.GiveIronAxe)
                    {
                        needAxe = true;

                        //if (_e.LessonT >= LessonTypes.GiveStaff)
                        //{
                        //    needStaff = true;

                        //    if (_e.LessonT >= LessonTypes.GiveBowCrossbow)
                        //    {
                        //        needBowCrossbow = true;

                        //        if (_e.LessonT >= LessonTypes.GiveShield)
                        //        {
                        //            needShield = true;

                        //            if (_e.LessonT >= LessonTypes.GiveSword)
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


                var tw_sel = _selectedToolWeaponC.ToolWeaponT;
                var levTw_sel = _selectedToolWeaponC.LevelT;

                color = _twE.ImageC(tw_sel).Image.color;
                color.a = 1;
                _twE.ImageC(tw_sel).Image.color = color;


                _twE.LevelImageC(tw_sel, levTw_sel).SetActive(true);


                for (var twT = ToolsWeaponsWarriorTypes.None + 1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                {
                    _twE.LevelImageC(twT, levTw_sel).SetActive(true);
                }

                var curPlayerI = _aboutGameC.CurrentPlayerIType;

                _twE.TextC(ToolsWeaponsWarriorTypes.Pick).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.First, ToolsWeaponsWarriorTypes.Pick).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Sword).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.Second, ToolsWeaponsWarriorTypes.Sword).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Axe).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.Second, ToolsWeaponsWarriorTypes.Axe).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Shield).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, _selectedToolWeaponC.LevelT, ToolsWeaponsWarriorTypes.Shield).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.BowCrossbow).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, _selectedToolWeaponC.LevelT, ToolsWeaponsWarriorTypes.BowCrossbow).ToString();
                _twE.TextC(ToolsWeaponsWarriorTypes.Staff).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.First, ToolsWeaponsWarriorTypes.Staff).ToString();
            }
        }
    }
}
