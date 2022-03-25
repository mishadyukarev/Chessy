using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System;

namespace Chessy.Game.System.Model.Master
{
    public sealed class SetConditionUnitS_M : SystemModelGameAbs
    {
        readonly BuildS _buildS;

        public SetConditionUnitS_M(in BuildS buildS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _buildS = buildS;
        }

        public void Set(in ConditionUnitTypes condT, in byte cell_0,  in Player sender)
        {
            switch (condT)
            {
                case ConditionUnitTypes.None:
                    eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    break;

                case ConditionUnitTypes.Protected:
                    if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (eMGame.UnitStepC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;
                        eMGame.UnitConditionTC(cell_0).Condition = condT;
                    }

                    else
                    {
                        eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;


                case ConditionUnitTypes.Relaxed:
                    if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else if (eMGame.UnitStepC(cell_0).Steps >= StepValues.FOR_TOGGLE_CONDITION_UNIT)
                    {
                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                        eMGame.UnitConditionTC(cell_0).Condition = condT;
                        eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_TOGGLE_CONDITION_UNIT;

                        if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        {
                            if (!eMGame.BuildingTC(cell_0).HaveBuilding)
                            {
                                if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (eMGame.UnitHpC(cell_0).Health >= HpValues.MAX)
                                    {
                                        if (eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            _buildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMGame.UnitPlayerTC(cell_0).Player, BuildingValues.MAX_HP, cell_0);
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