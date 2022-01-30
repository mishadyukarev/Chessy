using Game.Common;
using System;

namespace Game.Game
{
    sealed class UpdateThirstyMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateThirstyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var unitEs = Es.CellEs.UnitEs;

            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;
                ref var hp_0 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health;
                ref var water_0 = ref Es.CellEs.UnitEs.StatEs.Water(idx_0).Water;

                ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;


                if (unit_0.Have && !unit_0.IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (Es.CellEs.RiverEs.River(idx_0).RiverTC.HaveRiver)
                        {
                            Es.CellEs.UnitEs.StatEs.Water(idx_0).SetMax(Es.CellEs.UnitEs.Main(idx_0), Es.UnitStatUpgradesEs);
                        }
                        else
                        {
                            Es.CellEs.UnitEs.StatEs.Water(idx_0).Water.Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.15f));


                            if (!water_0.Have)
                            {
                                float percent = 0;
                                switch (Es.CellEs.UnitEs.Main(idx_0).UnitC.Unit)
                                {
                                    case UnitTypes.None: throw new Exception();
                                    case UnitTypes.King: percent = 0.4f; break;
                                    case UnitTypes.Pawn: percent = 0.5f; break;
                                    case UnitTypes.Archer: percent = 0.5f; break;
                                    case UnitTypes.Scout: percent = 0.5f; break;
                                    case UnitTypes.Elfemale: percent = 0.5f; break;
                                    case UnitTypes.Snowy: percent = 0.5f; break;
                                    default: throw new Exception();
                                }
                                Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * percent));


                                if (!hp_0.Have)
                                {
                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        Es.WhereBuildingEs.HaveBuild(Es.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                                        Es.CellEs.BuildEs.Build(idx_0).Remove();
                                    }
                                    unitEs.Kill(idx_0, Es);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}