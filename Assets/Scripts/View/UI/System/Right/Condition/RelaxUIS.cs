using Chessy.Model.Entity.View.UI.Right;
using Chessy.Model.Model.Entity;
using UnityEngine;

namespace Chessy.Model
{
    sealed class RelaxUIS : SystemUIAbstract
    {
        readonly RelaxUIE _relaxUIE;

        readonly bool[] _needActiveZone = new bool[(byte)UnitTypes.End];

        internal RelaxUIS(in RelaxUIE relaxUIE, in EntitiesModel eMG) : base(eMG)
        {
            _relaxUIE = relaxUIE;
        }

        internal override void Sync()
        {
            var idx_0 = _e.CellsC.Selected;

            var activeButt = false;


            for (var i = 0; i < _needActiveZone.Length; i++)
            {
                _needActiveZone[i] = false;
            }


            if (_e.UnitT(idx_0).HaveUnit())
            {
                if (_e.UnitPlayerT(idx_0).Is(_e.CurPlayerIT))
                {
                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.RelaxExtractPawn)
                    {
                        activeButt = true;

                        _relaxUIE.ImageC.Image.color = _e.UnitConditionT(idx_0).Is(ConditionUnitTypes.Relaxed) ? Color.green : Color.white;

                        if (_e.UnitT(idx_0).Is(UnitTypes.Pawn))
                        {
                            if (_e.MainToolWeaponT(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                _needActiveZone[(byte)_e.UnitT(idx_0)] = true;
                            }
                        }
                        else
                        {
                            _needActiveZone[(byte)_e.UnitT(idx_0)] = true;
                        }
                    }
                }
            }

            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                _relaxUIE.Button(unitT).SetActive(_needActiveZone[(byte)unitT]);
            }

            _relaxUIE.ButtonC.SetActiveParent(activeButt);
        }
    }
}