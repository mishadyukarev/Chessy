using Chessy.Game.Model.Entity;
using Chessy.Game.Entity.View.UI.Right;
using UnityEngine;

namespace Chessy.Game
{
    sealed class RelaxUIS : SystemUIAbstract
    {
        readonly RelaxUIE _relaxUIE;

        readonly bool[] _needActiveZone = new bool[(byte)UnitTypes.End];

        internal RelaxUIS(in RelaxUIE relaxUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _relaxUIE = relaxUIE;
        }

        internal override void Sync()
        {
            var idx_0 = e.CellsC.Selected;

            var activeButt = false;


            for (var i = 0; i < _needActiveZone.Length; i++)
            {
                _needActiveZone[i] = false;
            }


            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.PlayerT))
                {
                    if (!e.LessonTC.HaveLesson || e.LessonT >= Enum.LessonTypes.RelaxExtractPawn)
                    {
                        activeButt = true;

                        _relaxUIE.ImageC.Image.color = e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed) ? Color.green : Color.white;

                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            if (e.MainToolWeaponTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                _needActiveZone[(byte)e.UnitTC(idx_0).UnitT] = true;
                            }
                        }
                        else
                        {
                            _needActiveZone[(byte)e.UnitTC(idx_0).UnitT] = true;
                        }
                    }
                }
            }

            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                _relaxUIE.Button(unitT).SetActive(_needActiveZone[(byte)unitT]);
            }

            _relaxUIE.ButtonC.Button.gameObject.SetActive(activeButt);
        }
    }
}