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
            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

                ref var fire_0 = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;

                ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;


                if (ownUnit_0.Is(Entities.WhoseMove.CurPlayerI))
                {
                    if (unit_0.Have)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.CircularAttack;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.BonusNear;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                                {
                                    if (fire_0.Have) Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.PutOutFirePawn;
                                    else Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.Seed;
                                }

                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.Farm;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Ability = AbilityTypes.Mine;

                                if (build_0.Have)
                                {
                                    if (ownBuild_0.Is(ownUnit_0.Player))
                                    {
                                        Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.DestroyBuilding;

                                        //if (!WhereBuildsE.IsSetted(BuildingTypes.City, ownUnit_0.Player, out var idx_city))
                                        //{
                                        //    Entities.CellEs.CellUnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.City;
                                        //}
                                    }
                                }

                                else
                                {
                                    if (Entities.WhereBuildingEs.IsSetted(BuildingTypes.City, ownUnit_0.Player, out var idx_city))
                                    {
                                        Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Reset();
                                    }
                                    else
                                    {
                                        Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Fourth, idx_0).AbilityC.Ability = AbilityTypes.City;
                                    }
                                }


                                break;

                            case UnitTypes.Archer:
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FireArcher;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.ChangeCornerArcher;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Scout:
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Elfemale:
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.StunElfemale;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.GrowAdultForest;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Ability = AbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = AbilityTypes.FreezeDirectEnemy;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = AbilityTypes.IceWall;
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Camel:
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                    Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                    Entities.CellEs.UnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                }
            }
        }
    }
}