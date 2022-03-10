using Chessy.Common;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AbilitySyncS : CellSystem, IEcsRunSystem
    {
        internal AbilitySyncS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            E.UnitEs(Idx).Ability(ButtonTypes.First).Reset();
            E.UnitEs(Idx).Ability(ButtonTypes.Second).Reset();
            E.UnitEs(Idx).Ability(ButtonTypes.Third).Reset();
            E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Reset();
            E.UnitEs(Idx).Ability(ButtonTypes.Fifth).Reset();

            if (E.UnitPlayerTC(Idx).Is(E.CurPlayerITC.Player))
            {
                if (E.UnitTC(Idx).HaveUnit)
                {
                    switch (E.UnitTC(Idx).Unit)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            E.CellEs(Idx).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.CircularAttack;
                            E.CellEs(Idx).UnitEs.Ability(ButtonTypes.Second).Ability = AbilityTypes.KingPassiveNearBonus;
                            break;

                        case UnitTypes.Pawn:

                            if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                E.UnitEs(Idx).Ability(ButtonTypes.First).Ability = AbilityTypes.FireArcher;
                                E.UnitEs(Idx).Ability(ButtonTypes.Second).Ability = AbilityTypes.ChangeCornerArcher;
                            }
                            else if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                if (E.AdultForestC(Idx).HaveAnyResources)
                                {
                                    if (E.HaveFire(Idx)) E.CellEs(Idx).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.PutOutFirePawn;
                                    else E.CellEs(Idx).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    E.CellEs(Idx).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.Seed;
                                }

                                E.UnitEs(Idx).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetFarm;

                                if (E.BuildingTC(Idx).HaveBuilding)
                                {
                                    E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                                }

                                //else
                                //{
                                //    if (E.BuildingsInfo(E.UnitPlayerTC(Idx).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                //    {
                                //        E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Reset();
                                //    }
                                //    else
                                //    {
                                //        E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.SetCity;
                                //    }
                                //}
                            }

                            break;

                        case UnitTypes.Elfemale:
                            E.UnitEs(Idx).Ability(ButtonTypes.First).Ability = AbilityTypes.StunElfemale;
                            E.UnitEs(Idx).Ability(ButtonTypes.Second).Ability = AbilityTypes.GrowAdultForest;

                            break;

                        case UnitTypes.Snowy:
                            E.UnitEs(Idx).Ability(ButtonTypes.First).Ability = AbilityTypes.DirectWave;
                            E.UnitEs(Idx).Ability(ButtonTypes.Second).Ability = AbilityTypes.ActiveAroundBonusSnowy;
                            E.UnitEs(Idx).Ability(ButtonTypes.Third).Ability = AbilityTypes.ChangeDirectionWind;
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third).Ability = AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            E.UnitEs(Idx).Ability(ButtonTypes.First).Ability = AbilityTypes.Resurrect;
                            E.UnitEs(Idx).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetTeleport;
                            E.UnitEs(Idx).Ability(ButtonTypes.Third).Ability = AbilityTypes.InvokeSkeletons;
                            if (E.BuildingTC(Idx).HaveBuilding) E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                            break;

                        case UnitTypes.Hell:
                            break;

                        case UnitTypes.Camel:
                            break;

                        case UnitTypes.Skeleton:
                            break;

                        default: throw new Exception();
                    }

                    if (E.BuildingTC(Idx).Is(BuildingTypes.Teleport))
                    {
                        E.UnitEs(Idx).Ability(ButtonTypes.Fifth).Ability = AbilityTypes.Teleport;
                    }

                }
            }
        }
    }
}