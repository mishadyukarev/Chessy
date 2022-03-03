using System;

namespace Chessy.Game
{
    sealed class AbilitySyncS : SystemAbstract, IEcsRunSystem
    {
        internal AbilitySyncS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.UnitEs(idx_0).Ability(ButtonTypes.First).Reset();
                E.UnitEs(idx_0).Ability(ButtonTypes.Second).Reset();
                E.UnitEs(idx_0).Ability(ButtonTypes.Third).Reset();
                E.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Reset();
                E.UnitEs(idx_0).Ability(ButtonTypes.Fifth).Reset();

                if (E.UnitPlayerTC(idx_0).Is(E.CurPlayerITC.Player))
                {
                    if (E.UnitTC(idx_0).HaveUnit)
                    {
                        switch (E.UnitTC(idx_0).Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                E.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.CircularAttack;
                                E.CellEs(idx_0).UnitEs.Ability(ButtonTypes.Second).Ability = AbilityTypes.BonusNear;
                                break;

                            case UnitTypes.Pawn:

                                if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                {
                                    E.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.FireArcher;
                                    E.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.ChangeCornerArcher;
                                }
                                else
                                {
                                    if (E.AdultForestC(idx_0).HaveAnyResources)
                                    {
                                        if (E.HaveFire(idx_0)) E.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.PutOutFirePawn;
                                        else E.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.FirePawn;
                                    }
                                    else
                                    {
                                        E.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.Seed;
                                    }

                                    E.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetFarm;

                                    if (E.BuildingTC(idx_0).HaveBuilding)
                                    {
                                        E.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                                    }

                                    else
                                    {
                                        if (E.BuildingsInfo(E.UnitPlayerTC(idx_0).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                        {
                                            E.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Reset();
                                        }
                                        else
                                        {
                                            E.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.SetCity;
                                        }
                                    }
                                }

                                break;

                            case UnitTypes.Scout:
                                break;

                            case UnitTypes.Elfemale:
                                E.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.StunElfemale;
                                E.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.GrowAdultForest;
                                E.UnitEs(idx_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                E.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.DirectWave;
                                E.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.ActiveAroundBonusSnowy;
                                E.UnitEs(idx_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.IceWall;
                                break;

                            case UnitTypes.Undead:
                                E.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.Resurrect;
                                E.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetTeleport;
                                E.UnitEs(idx_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.InvokeSkeletons;
                                if (E.BuildingTC(idx_0).HaveBuilding) E.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                                break;

                            case UnitTypes.Hell:
                                break;

                            case UnitTypes.Camel:
                                break;

                            case UnitTypes.Skeleton:
                                break;

                            default: throw new Exception();
                        }

                        if (E.BuildingTC(idx_0).Is(BuildingTypes.Teleport))
                        {
                            E.UnitEs(idx_0).Ability(ButtonTypes.Fifth).Ability = AbilityTypes.Teleport;
                        }

                    }
                }
            }
        }
    }
}