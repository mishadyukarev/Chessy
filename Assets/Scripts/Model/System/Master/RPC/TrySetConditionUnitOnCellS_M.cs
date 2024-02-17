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
                        unitCs[cellIdx].ConditionT = ConditionUnitTypes.None;
                        break;

                    case ConditionUnitTypes.Protected:
                        if (unitCs[cellIdx].ConditionT == ConditionUnitTypes.Protected)
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            unitCs[cellIdx].ConditionT = ConditionUnitTypes.None;
                        }
                        else
                        {
                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            unitCs[cellIdx].ConditionT = condT;

                            //if (_aboutGameC.LessonT == LessonTypes.ClickDefend) SetNextLesson();
                        }
                        break;


                    case ConditionUnitTypes.Relaxed:
                        if (unitCs[cellIdx].ConditionT == ConditionUnitTypes.Relaxed)
                        {
                            //RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
                            unitCs[cellIdx].ConditionT = ConditionUnitTypes.None;
                        }
                        else
                        {
                            unitCs[cellIdx].ConditionT = condT;

                            var clipT = ClipTypes.SighUnit;

                            if (unitCs[cellIdx].UnitT == UnitTypes.Pawn)
                            {
                                if (environmentCs[cellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    if (!buildingCs[cellIdx].HaveBuilding)
                                    {

                                        if (unitHpCs[cellIdx].Health >= HpUnitValues.MAX)
                                        {
                                            if (PlayerInfoE(unitCs[cellIdx].PlayerT).GodInfoC.UnitType.Is(UnitTypes.Elfemale))
                                            {
                                                Build(BuildingTypes.Woodcutter, LevelTypes.First, UnitC(cellIdx).PlayerT, cellIdx);
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

                unitCs[cellIdx].HowManySecondUnitWasHereInThisCondition = 0;
            }

            else
            {
                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}