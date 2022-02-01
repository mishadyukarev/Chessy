using System;

namespace Game.Game
{
    sealed class AbilitySyncS : SystemCellAbstract, IEcsRunSystem
    {
        public AbilitySyncS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var cellEs = Es.CellEs;


            foreach (byte idx_0 in cellEs.Idxs)
            {
                var unit_0 = UnitEs.Main(idx_0).UnitTC;
                var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;

                ref var fire_0 = ref cellEs.FireEs.Fire(idx_0).Fire;

                var build_0 = BuildEs.BuildingE(idx_0).BuildTC;
                var ownBuild_0 = BuildEs.BuildingE(idx_0).Owner;


                if (ownUnit_0.Is(Es.WhoseMove.CurPlayerI))
                {
                    if (UnitEs.Main(idx_0).HaveUnit(UnitEs.StatEs))
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.CircularAttack;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.BonusNear;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (cellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                                {
                                    if (fire_0.Have) cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.PutOutFirePawn;
                                    else cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.Seed;
                                }

                                cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.Farm;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Ability = AbilityTypes.Mine;

                                if (build_0.Have)
                                {
                                    if (ownBuild_0.Is(ownUnit_0.Player))
                                    {
                                        cellEs.UnitEs.AbilityButton(ButtonTypes.Fourth, idx_0).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        cellEs.UnitEs.AbilityButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.DestroyBuilding;
                                    }
                                }

                                else
                                {
                                    if (Es.WhereBuildingEs.IsSetted(BuildingTypes.City, ownUnit_0.Player, out var idx_city))
                                    {
                                        cellEs.UnitEs.AbilityButton(ButtonTypes.Fourth, idx_0).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        cellEs.UnitEs.AbilityButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.City;
                                    }
                                }


                                break;

                            case UnitTypes.Archer:
                                cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FireArcher;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.ChangeCornerArcher;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Scout:
                                cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Elfemale:
                                cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.StunElfemale;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.GrowAdultForest;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Ability = AbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.IceWall;
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Camel:
                                cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    cellEs.UnitEs.AbilityButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                    cellEs.UnitEs.AbilityButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                    cellEs.UnitEs.AbilityButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                }
            }
        }
    }
}