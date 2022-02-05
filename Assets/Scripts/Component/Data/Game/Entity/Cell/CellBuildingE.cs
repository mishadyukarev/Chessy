using ECS;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellBuildingE : CellEntityAbstract
    {
        ref AmountC HealthRef => ref Ent.Get<AmountC>();
        ref BuildingTC BuildTCRef => ref Ent.Get<BuildingTC>();
        ref PlayerTC PlayerTCRef => ref Ent.Get<PlayerTC>();

        public AmountC Health => Ent.Get<AmountC>();
        public BuildingTC BuildTC => Ent.Get<BuildingTC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();

        public bool Is(params BuildingTypes[] builds) => BuildTC.Is(builds);
        public bool Is(params PlayerTypes[] owners) => OwnerC.Is(owners);
        public bool HaveBuilding => !BuildTC.Is(BuildingTypes.None, BuildingTypes.End);
        public bool IsAlive => Health.Amount > 0;


        public bool CanExtractFertilizer(in CellEnvironmentEs envEs)
        {
            if (HaveBuilding && BuildTC.Is(BuildingTypes.Farm)
                && envEs.Fertilizer.HaveEnvironment) return true;
            else return false;
        }

        internal CellBuildingE(in byte idx, in EcsWorld world) : base(idx, world)
        {
        }

        public void SetNew(in BuildingTypes build, in PlayerTypes owner)
        {
            if (HaveBuilding) throw new Exception("There's got building on cell");

            BuildTCRef.Build = build;
            PlayerTCRef.Player = owner;
            HealthRef.Amount = CellBuildingValues.MaxAmountHealth(build);
        }
        public void Destroy()
        {
            BuildTCRef.Build = BuildingTypes.None;
            PlayerTCRef.Player = PlayerTypes.None;
            HealthRef.Amount = 0;
        }

        public void Defrost()
        {
            if (!HaveBuilding) throw new Exception("There's not got building on cell");
            if (!Is(BuildingTypes.IceWall)) throw new Exception("Need Ice Wall on cell");

            HealthRef.Amount--;
            if (!IsAlive) Destroy();
        }

        public void Sync(in int health, in BuildingTypes build, in PlayerTypes player)
        {
            HealthRef.Amount = health;
            BuildTCRef.Build = build;
            PlayerTCRef.Player = player;
        }
    }
}