using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.UI.System
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
            var idx_0 = indexesCellsC.Selected;

            var activeButt = false;


            for (var i = 0; i < _needActiveZone.Length; i++)
            {
                _needActiveZone[i] = false;
            }


            if (unitCs[idx_0].HaveUnit)
            {
                if (unitCs[idx_0].PlayerType == aboutGameC.CurrentPlayerIType)
                {
                    if (!aboutGameC.LessonType.HaveLesson() || aboutGameC.LessonType >= LessonTypes.RelaxExtractPawn)
                    {
                        activeButt = true;

                        _relaxUIE.ImageC.Image.color = unitCs[idx_0].ConditionType == ConditionUnitTypes.Relaxed ? Color.green : Color.white;

                        if (unitCs[idx_0].UnitType == UnitTypes.Pawn)
                        {
                            if (_mainTWC[idx_0].ToolWeaponType == ToolsWeaponsWarriorTypes.Axe)
                            {
                                _needActiveZone[(byte)unitCs[idx_0].UnitType] = true;
                            }
                        }
                        else
                        {
                            _needActiveZone[(byte)unitCs[idx_0].UnitType] = true;
                        }
                    }
                }
            }

            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                _relaxUIE.Button(unitT).TrySetActive(_needActiveZone[(byte)unitT]);
            }

            _relaxUIE.ButtonC.SetActiveParent(activeButt);
        }
    }
}