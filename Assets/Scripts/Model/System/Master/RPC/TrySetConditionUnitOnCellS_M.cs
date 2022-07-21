using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Realtime;
using System;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TrySetConditionUnitOnCellM(in ConditionUnitTypes condT, in byte cellIdx, in Player sender)
        {
            if (!_effectsUnitCs[cellIdx].IsStunned)
            {
                switch (condT)
                {
                    case ConditionUnitTypes.None:
                        _unitCs[cellIdx].ConditionT = ConditionUnitTypes.None;
                        break;

                    case ConditionUnitTypes.Protected:
                        if (_unitCs[cellIdx].ConditionT == ConditionUnitTypes.Protected)
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _unitCs[cellIdx].ConditionT = ConditionUnitTypes.None;
                        }
                        else
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _unitCs[cellIdx].ConditionT = condT;

                            //if (_aboutGameC.LessonT == LessonTypes.ClickDefend) SetNextLesson();
                        }
                        break;


                    case ConditionUnitTypes.Relaxed:
                        if (_unitCs[cellIdx].ConditionT == ConditionUnitTypes.Relaxed)
                        {
                            //RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            _unitCs[cellIdx].ConditionT = ConditionUnitTypes.None;
                        }
                        else
                        {
                            _unitCs[cellIdx].ConditionT = condT;

                            var clipT = ClipTypes.SighUnit;

                            if (_unitCs[cellIdx].UnitT == UnitTypes.Pawn)
                            {
                                if (_environmentCs[cellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    if (!_buildingCs[cellIdx].HaveBuilding)
                                    {

                                        if (_hpUnitCs[cellIdx].Health >= HpUnitValues.MAX)
                                        {
                                            if (PlayerInfoE(_unitCs[cellIdx].PlayerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                                            {
                                                _e.Build(BuildingTypes.Woodcutter, LevelTypes.First, _unitCs[cellIdx].PlayerT, ValuesChessy.MAX_HP_ANY_BUILDING, cellIdx);
                                            }
                                        }
                                    }

                                    clipT = ClipTypes.ExtractAdultForestWithWarrior;

                                    RpcSs.AnimationCellToGeneral(cellIdx, AnimationCellTypes.ExtractWood, sender);
                                }
                            }

                            RpcSs.ExecuteSoundActionToGeneral(sender, clipT);
                        }
                        break;

                    default:
                        throw new Exception();
                }

                _unitCs[cellIdx].HowManySecondUnitWasHereInThisCondition = 0;
            }

            else
            {
                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}