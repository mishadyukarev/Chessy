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
            var unitEs = UnitEs;

            foreach (byte idx_0 in CellEs.Idxs)
            {
                var unit_0 = UnitEs.Main(idx_0).UnitTC;
                var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;

                var build_0 = BuildEs.BuildingE(idx_0).BuildTC;


                if (UnitEs.Main(idx_0).HaveUnit(UnitStatEs) && !unit_0.IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (CellEs.RiverEs.River(idx_0).RiverTC.HaveRiver)
                        {
                            UnitEs.StatEs.Water(idx_0).SetMax(UnitEs.Main(idx_0), Es.UnitStatUpgradesEs);
                        }
                        else
                        {
                            UnitEs.StatEs.Water(idx_0).Water.Amount -= (int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.15f);


                            if (!UnitEs.StatEs.Water(idx_0).HaveWater)
                            {
                                float percent = 0;
                                switch (UnitEs.Main(idx_0).UnitTC.Unit)
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
                                UnitEs.StatEs.Hp(idx_0).Health.Amount -=(int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * percent);


                                if (!UnitEs.StatEs.Hp(idx_0).IsAlive)
                                {
                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        Es.WhereBuildingEs.HaveBuild(BuildEs.BuildingE(idx_0), idx_0).HaveBuilding.Have = false;
                                        BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);
                                    }
                                    unitEs.Main(idx_0).Kill(Es);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}