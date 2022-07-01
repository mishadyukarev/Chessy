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


            _twE.ParentGOC.SetActive(needActiveZone);


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

                        if (_e.LessonT >= LessonTypes.GiveStaff)
                        {
                            needStaff = true;

                            if (_e.LessonT >= LessonTypes.GiveBowCrossbow)
                            {
                                needBowCrossbow = true;

                                if (_e.LessonT >= LessonTypes.GiveShield)
                                {
                                    needShield = true;

                                    if (_e.LessonT >= LessonTypes.GiveSword)
                                    {
                                        needSword = true;


                                    }
                                }
                            }
                        }
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


                var tw_sel = _e.SelectedE.ToolWeaponC.ToolWeaponT;
                var levTw_sel = _e.SelectedE.ToolWeaponC.LevelT;

                color = _twE.ImageC(tw_sel).Image.color;
                color.a = 1;
                _twE.ImageC(tw_sel).Image.color = color;


                _twE.LevelImageC(tw_sel, levTw_sel).SetActive(true);


                for (var twT = ToolWeaponTypes.None + 1; twT < ToolWeaponTypes.End; twT++)
                {
                    _twE.LevelImageC(twT, levTw_sel).SetActive(true);
                }

                var curPlayerI = _e.CurrentPlayerIT;

                _twE.TextC(ToolWeaponTypes.Pick).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.First, ToolWeaponTypes.Pick).ToString();
                _twE.TextC(ToolWeaponTypes.Sword).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.Second, ToolWeaponTypes.Sword).ToString();
                _twE.TextC(ToolWeaponTypes.Axe).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.Second, ToolWeaponTypes.Axe).ToString();
                _twE.TextC(ToolWeaponTypes.Shield).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, _e.SelectedE.ToolWeaponC.LevelT, ToolWeaponTypes.Shield).ToString();
                _twE.TextC(ToolWeaponTypes.BowCrossbow).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, _e.SelectedE.ToolWeaponC.LevelT, ToolWeaponTypes.BowCrossbow).ToString();
                _twE.TextC(ToolWeaponTypes.Staff).TextUI.text = _e.ToolWeaponsInInventor(curPlayerI, LevelTypes.First, ToolWeaponTypes.Staff).ToString();
            }
        }
    }
}
