using System;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct AbilitySyncS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

                ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;

                ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;


                if (ownUnit_0.Is(Entities.WhoseMove.CurPlayerI))
                {
                    if (unit_0.Have)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.CircularAttack;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.BonusNear;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                                {
                                    if (fire_0.Have) CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.PutOutFirePawn;
                                    else CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.Seed;
                                }

                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.Farm;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Ability = AbilityTypes.Mine;

                                if (build_0.Have)
                                {
                                    if (ownBuild_0.Is(ownUnit_0.Player))
                                    {
                                        CellUnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        CellUnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.DestroyBuilding;

                                        //if (!WhereBuildsE.IsSetted(BuildingTypes.City, ownUnit_0.Player, out var idx_city))
                                        //{
                                        //    CellUnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.City;
                                        //}
                                    }
                                }

                                else
                                {
                                    if (WhereBuildsE.IsSetted(BuildingTypes.City, ownUnit_0.Player, out var idx_city))
                                    {
                                        CellUnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        CellUnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.City;
                                    }
                                }


                                break;

                            case UnitTypes.Archer:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FireArcher;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.ChangeCornerArcher;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Scout:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Elfemale:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.StunElfemale;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.GrowAdultForest;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Ability = AbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FreezeDirectEnemy;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.IceWall;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Camel:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                    CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                    CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                }
            }
        }
    }
}