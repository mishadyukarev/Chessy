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
                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

                var build_0 = BuildEs(idx_0).BuildingE.BuildTC;
                var ownBuild_0 = BuildEs(idx_0).BuildingE.OwnerC;


                if (ownUnit_0.Is(Es.WhoseMove.CurPlayerI))
                {
                    if (UnitEs(idx_0).MainE.HaveUnit(UnitEs(idx_0).StatEs))
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.CircularAttack;
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.BonusNear;
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Third).AbilityC.Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (CellEs(idx_0).EnvironmentEs.AdultForest.HaveEnvironment)
                                {
                                    if (EffectEs(idx_0).FireE.HaveFireC.Have) CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.PutOutFirePawn;
                                    else CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.Seed;
                                }

                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.Farm;
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Third).AbilityC.Ability = AbilityTypes.Mine;

                                if (build_0.Have)
                                {
                                    if (ownBuild_0.Is(ownUnit_0.Player))
                                    {
                                        CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Fourth).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Fourth).AbilityC.Ability = AbilityTypes.DestroyBuilding;
                                    }
                                }

                                else
                                {
                                    if (Es.WhereBuildingEs.TryGetBuilding(BuildingTypes.City, ownUnit_0.Player, out var idx_city))
                                    {
                                        CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Fourth).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Fourth).AbilityC.Ability = AbilityTypes.City;
                                    }
                                }


                                break;

                            case UnitTypes.Archer:
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.FireArcher;
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.ChangeCornerArcher;
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Third).AbilityC.Reset();
                                break;

                            case UnitTypes.Scout:
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Reset();
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Reset();
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Third).AbilityC.Reset();
                                break;

                            case UnitTypes.Elfemale:
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.StunElfemale;
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.GrowAdultForest;
                                CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Third).AbilityC.Ability = AbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                UnitEs(idx_0).AbilityButton(ButtonTypes.First).AbilityC.Ability = AbilityTypes.DirectWave;
                                UnitEs(idx_0).AbilityButton(ButtonTypes.Second).AbilityC.Ability = AbilityTypes.ActiveAroundBonusSnowy;
                                UnitEs(idx_0).AbilityButton(ButtonTypes.Third).AbilityC.Ability = AbilityTypes.IceWall;
                                break;

                            case UnitTypes.Camel:
                                UnitEs(idx_0).AbilityButton(ButtonTypes.First).AbilityC.Reset();
                                UnitEs(idx_0).AbilityButton(ButtonTypes.Second).AbilityC.Reset();
                                UnitEs(idx_0).AbilityButton(ButtonTypes.Third).AbilityC.Reset();
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.First).AbilityC.Reset();
                    CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Second).AbilityC.Reset();
                    CellEs(idx_0).UnitEs.AbilityButton(ButtonTypes.Third).AbilityC.Reset();
                }
            }
        }
    }
}