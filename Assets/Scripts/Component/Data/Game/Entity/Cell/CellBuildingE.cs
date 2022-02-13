using ECS;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellBuildingE : CellEntityAbstract
    {
        ref HealthC HealthC => ref Ent.Get<HealthC>();
        ref BuildingTC BuildingC => ref Ent.Get<BuildingTC>();
        ref PlayerTC PlayerC => ref Ent.Get<PlayerTC>();
        ref IsActiveSmelterC IsActiveSmelterC => ref Ent.Get<IsActiveSmelterC>();


        public BuildingTypes Building
        {
            get => BuildingC.Build;
            set => BuildingC.Build = value;
        }
        public PlayerTypes Owner
        {
            get => PlayerC.Player;
            set => PlayerC.Player = value;
        }
        public float Health
        {
            get => HealthC.Health;
            set => HealthC.Health = value;
        }
        public bool IsActiveSmelter
        {
            get => IsActiveSmelterC.IsActive;
            set => IsActiveSmelterC.IsActive = value;
        }

        public bool Is(params BuildingTypes[] builds) => BuildingC.Is(builds);
        public bool Is(params PlayerTypes[] owners) => PlayerC.Is(owners);

        public bool HaveBuilding => !Is(BuildingTypes.None, BuildingTypes.End);
        public bool IsAlive => Health > 0;


        public bool CanExtractFertilizer(in CellEnvironmentEs envEs)
        {
            if (HaveBuilding && BuildingC.Is(BuildingTypes.Farm)
                && envEs.Fertilizer.HaveEnvironment) return true;
            else return false;
        }

        internal CellBuildingE(in byte idx, in EcsWorld world) : base(idx, world)
        {
        }

        public void SetNew(in BuildingTypes buildT, in PlayerTypes ownerT)
        {
            if (HaveBuilding) throw new Exception("There's got building on cell");

            BuildingC.Build = buildT;
            PlayerC.Player = ownerT;
            HealthC.Health = CellBuildingValues.MaxAmountHealth(buildT);
        }
        public void SetNewHouse(in PlayerTypes owner, in MaxAvailablePawnsE maxPawnsE)
        {
            SetNew(BuildingTypes.House, owner);
            maxPawnsE.Add();
        }
        public void SetNewSmelter(in PlayerTypes owner)
        {
            SetNew(BuildingTypes.Smelter, owner);
            IsActiveSmelterC.IsActive = false;
        }

        public void Destroy(in Entities ents)
        {
            if (BuildingC.Is(BuildingTypes.Teleport))
            {
                if (ents.StartTeleportE.Where == Idx) ents.StartTeleportE.Reset();
                else ents.EndTeleportE.Reset();
            }

            BuildingC.Build = BuildingTypes.None;
            PlayerC.Player = PlayerTypes.None;
            HealthC.Health = 0;
        }

        public void Sync(in int health, in BuildingTypes build, in PlayerTypes player)
        {
            HealthC.Health = health;
            BuildingC.Build = build;
            PlayerC.Player = player;
        }
        public void ToggleSmelter() => IsActiveSmelterC.IsActive = !IsActiveSmelterC.IsActive;

        public void Build_Master(in byte idx_to_0, in BuildingTypes buildT, in Player sender, in Entities ents)
        {
            var idx_from = Idx;

            var whoseMove = ents.WhoseMoveE.WhoseMove.Player;


            foreach (var idx_to_1 in ents.CellSpaceWorker.GetIdxsAround(idx_to_0))
            {
                if (ents.BuildingE(idx_to_1).Is(BuildingTypes.City, BuildingTypes.House, BuildingTypes.Market, BuildingTypes.Smelter))
                {
                    ents.CellSpaceWorker.TryGetDirect(idx_to_0, idx_to_1, out var dir);

                    if (dir == DirectTypes.Left || dir == DirectTypes.Right || dir == DirectTypes.Up || dir == DirectTypes.Down)
                    {
                        if (ents.InventorResourcesEs.CanBuyBuilding_Master(buildT, whoseMove, out var needRes))
                        {
                            ents.InventorResourcesEs.BuyBuilding_Master(buildT, whoseMove);

                            if (buildT == BuildingTypes.House)
                            {
                                ents.BuildingE(idx_to_0).SetNewHouse(whoseMove, ents.MaxAvailablePawnsE(whoseMove));
                            }
                            else if (buildT == BuildingTypes.Smelter)
                            {
                                ents.BuildingE(idx_to_0).SetNewSmelter(whoseMove);
                            }
                            else
                            {
                                ents.BuildingE(idx_to_0).SetNew(buildT, whoseMove);
                            }


                            
                            break;
                        }
                        else
                        {
                            ents.RpcE.MistakeEconomyToGeneral(sender, needRes);
                        }
                    }
                }
            } 
        }
    }
}