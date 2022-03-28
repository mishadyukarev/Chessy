using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Game.System.Model.Master
{
    sealed class SetConditionUnitS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;
        readonly BuildS _buildS;

        internal SetConditionUnitS_M(in CellEs cellEs, in BuildS buildS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
            _buildS = buildS;
        }

        internal void Set(in ConditionUnitTypes condT, in Player sender)
        {
            switch (condT)
            {
                case ConditionUnitTypes.None:
                    _cellEs.UnitMainE.ConditionTC.Condition = ConditionUnitTypes.None;
                    break;

                case ConditionUnitTypes.Protected:
                    if (_cellEs.UnitMainE.ConditionTC.Is(ConditionUnitTypes.Protected))
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        _cellEs.UnitMainE.ConditionTC.Condition = ConditionUnitTypes.None;
                    }

                    else if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                        _cellEs.UnitMainE.ConditionTC.Condition = condT;
                    }

                    else
                    {
                        eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (_cellEs.UnitMainE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        _cellEs.UnitMainE.ConditionTC.Condition = ConditionUnitTypes.None;
                    }

                    else if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        _cellEs.UnitMainE.ConditionTC.Condition = condT;
                        _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                        if (_cellEs.UnitMainE.UnitTC.Is(UnitTypes.Pawn))
                        {
                            if (!_cellEs.BuildEs.MainE.BuildingTC.HaveBuilding)
                            {
                                if (_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
                                {
                                    if (_cellEs.UnitStatsE.HealthC.Health >= HpValues.MAX)
                                    {
                                        if (eMGame.PlayerInfoE(_cellEs.UnitMainE.PlayerTC.Player).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            _buildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, _cellEs.UnitMainE.PlayerTC.Player, BuildingValues.MAX_HP);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}