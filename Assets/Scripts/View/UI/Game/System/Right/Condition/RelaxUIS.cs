using Chessy.Game.Model.Entity;
using Chessy.Game.Entity.View.UI.Right;
using UnityEngine;

namespace Chessy.Game
{
    sealed class RelaxUIS : SystemUIAbstract
    {
        readonly RelaxUIE _relaxUIE;

        internal RelaxUIS(in RelaxUIE relaxUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _relaxUIE = relaxUIE;
        }

        internal override void Sync()
        {
            var idx_0 = e.CellsC.Selected;

            var activeButt = false;

            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.PlayerT))
                {
                    if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= Enum.LessonTypes.RelaxExtractPawn)
                    {
                        activeButt = true;

                        _relaxUIE.ImageC.Image.color = e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed) ? Color.green : Color.white;

                        for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                        {
                            _relaxUIE.Button(unitT).SetActive(false);
                        }

                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            if (e.MainToolWeaponTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                _relaxUIE.Button(e.UnitTC(idx_0).UnitT).SetActive(true);
                            }
                            else
                            {
                                _relaxUIE.Button(e.UnitTC(idx_0).UnitT).SetActive(false);
                            }
                        }
                        else
                        {
                            _relaxUIE.Button(e.UnitTC(idx_0).UnitT).SetActive(true);
                        }
                    }
                }
            }

            _relaxUIE.ButtonC.Button.gameObject.SetActive(activeButt);
        }
    }
}