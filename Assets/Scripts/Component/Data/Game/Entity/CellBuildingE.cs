using ECS;
using System;

namespace Game.Game
{
    public sealed class CellBuildingE : CellAbstE
    {
        ref AmountC HealthRef => ref Ent.Get<AmountC>();
        ref BuildingTC BuildTCRef => ref Ent.Get<BuildingTC>();
        ref PlayerTC PlayerTCRef => ref Ent.Get<PlayerTC>();

        public AmountC Health => Ent.Get<AmountC>();
        public BuildingTC BuildTC => Ent.Get<BuildingTC>();
        public PlayerTC Owner => Ent.Get<PlayerTC>();

        public bool HaveBuilding => IsAlive && HaveBuildingT;
        public bool IsAlive => Health.Amount > 0;
        public bool HaveBuildingT => BuildTC.Have;

        public bool CanExtractAdultForest(in CellBuildEs buildEs, in CellEnvironmentEs envEs)
        {
            if (buildEs.BuildingE(Idx).HaveBuilding
                && buildEs.BuildingE(Idx).BuildTC.Is(BuildingTypes.Woodcutter)
                && envEs.AdultForest(Idx).HaveEnvironment) return true;
            else return false;
        }
        public bool CanExtractFertilizer(in CellEnvironmentEs envEs)
        {
            if (HaveBuilding && BuildTC.Is(BuildingTypes.Farm)
                && envEs.Fertilizer(Idx).HaveEnvironment) return true;
            else return false;
        }

        internal CellBuildingE(in byte idx, in EcsWorld world) : base(idx, world)
        {
        }

        public void SetNew(in BuildingTypes build, in PlayerTypes owner, in CellBuildEs buildEs, in WhereBuildingEs whereBuildingEs)
        {
            if (buildEs.BuildingE(Idx).HaveBuilding) throw new Exception("There's got building on cell");

            BuildTCRef.Build = build;
            PlayerTCRef.Player = owner;
            HealthRef.Amount = CellBuildingValues.MaxAmountHealth(build);

            whereBuildingEs.HaveBuild(build, owner, Idx).HaveBuilding.Have = true;
        }
        public void Destroy(in CellBuildEs buildEs, in WhereBuildingEs whereBuildingEs)
        {
            //if (!buildEs.BuildingE(Idx).HaveBuilding) throw new Exception("There's not got building on cell");

            whereBuildingEs.HaveBuild(buildEs.BuildingE(Idx), Idx).HaveBuilding.Have = false;

            BuildTCRef.Build = BuildingTypes.None;
            PlayerTCRef.Player = PlayerTypes.None;
            HealthRef.Amount = 0;
        }

        public void Defrost(in CellBuildEs buildEs, in WhereBuildingEs whereBuildingEs)
        {
            if (!buildEs.BuildingE(Idx).HaveBuilding) throw new Exception("There's not got building on cell");
            if (!buildEs.BuildingE(Idx).BuildTC.Is(BuildingTypes.IceWall)) throw new Exception("Need Ice Wall on cell");

            HealthRef.Amount--;
            if (!buildEs.BuildingE(Idx).IsAlive) Destroy(buildEs, whereBuildingEs);
        }

        public void Sync(in int health, in BuildingTypes build, in PlayerTypes player)
        {
            HealthRef.Amount = health;
            BuildTCRef.Build = build;
            PlayerTCRef.Player = player;
        }
    }
}