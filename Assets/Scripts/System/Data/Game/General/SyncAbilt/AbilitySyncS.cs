using System;

namespace Game.Game
{
    sealed class AbilitySyncS : SystemAbstract, IEcsRunSystem
    {
        internal AbilitySyncS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.First).AbilityC.Reset();
                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Second).AbilityC.Reset();
                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Third).AbilityC.Reset();
                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Fourth).AbilityC.Reset();
                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Fifth).AbilityC.Reset();

                if (Es.UnitPlayerTC(idx_0).Is(Es.CurPlayerI.Player))
                {
                    if (Es.UnitTC(idx_0).HaveUnit)
                    {
                        switch (Es.UnitTC(idx_0).Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.CircularAttack;
                                Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.BonusNear;
                                break;

                            case UnitTypes.Pawn:

                                if (Es.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                {
                                    Es.UnitEs(idx_0).AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.FireArcher;
                                    Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.ChangeCornerArcher;
                                }
                                else
                                {
                                    if (Es.AdultForestC(idx_0).HaveAny)
                                    {
                                        if (Es.HaveFire(idx_0)) Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.PutOutFirePawn;
                                        else Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.FirePawn;
                                    }
                                    else
                                    {
                                        Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.Seed;
                                    }

                                    Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.SetFarm;

                                    if (Es.BuildTC(idx_0).HaveBuilding)
                                    {
                                        Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Fourth).AbilityC.Ability = AbilityTypes.DestroyBuilding;
                                    }

                                    else
                                    {
                                        if (Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).LevelE(LevelTypes.First).BuildsInGame(BuildingTypes.City).HaveAny)
                                        {
                                            Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Fourth).AbilityC.Reset();
                                        }
                                        else
                                        {
                                            Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Fourth).AbilityC.Ability = AbilityTypes.SetCity;
                                        }
                                    }
                                }

                                break;

                            case UnitTypes.Scout:
                                break;

                            case UnitTypes.Elfemale:
                                Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.StunElfemale;
                                Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.GrowAdultForest;
                                Es.CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Third).AbilityC.Ability = AbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.DirectWave;
                                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.ActiveAroundBonusSnowy;
                                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Third).AbilityC.Ability = AbilityTypes.IceWall;
                                break;

                            case UnitTypes.Undead:
                                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.Resurrect;
                                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.SetTeleport;
                                Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Third).AbilityC.Ability = AbilityTypes.InvokeSkeletons;
                                if (Es.BuildTC(idx_0).HaveBuilding) Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Fourth).AbilityC.Ability = AbilityTypes.DestroyBuilding;
                                break;

                            case UnitTypes.Hell:
                                break;

                            case UnitTypes.Camel:
                                break;

                            case UnitTypes.Skeleton:
                                break;

                            default: throw new Exception();
                        }

                        if (Es.BuildTC(idx_0).Is(BuildingTypes.Teleport))
                        {
                            Es.UnitEs(idx_0).AbilityButton(ButtonTypes.Fifth).AbilityC.Ability = AbilityTypes.Teleport;
                        }

                    }
                }
            }
        }
    }
}