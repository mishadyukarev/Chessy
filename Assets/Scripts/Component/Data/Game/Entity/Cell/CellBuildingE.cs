using ECS;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellBuildingE : CellEntityAbstract
    {
        ref AmountC HealthRef => ref Ent.Get<AmountC>();
        ref BuildingTC BuildTCRef => ref Ent.Get<BuildingTC>();
        ref PlayerTC PlayerTCRef => ref Ent.Get<PlayerTC>();
        ref IsActiveSmelterC IsActiveSmelterCRef => ref Ent.Get<IsActiveSmelterC>();

        public AmountC Health => Ent.Get<AmountC>();
        public BuildingTC BuildTC => Ent.Get<BuildingTC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();
        public IsActiveSmelterC IsActiveSmelterC => Ent.Get<IsActiveSmelterC>();

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

        public void SetNew(in BuildingTypes buildT, in PlayerTypes ownerT)
        {
            if (HaveBuilding) throw new Exception("There's got building on cell");

            BuildTCRef.Build = buildT;
            PlayerTCRef.Player = ownerT;
            HealthRef.Amount = CellBuildingValues.MaxAmountHealth(buildT);
        }
        public void SetNewHouse(in PlayerTypes owner, in MaxAvailablePawnsE maxPawnsE)
        {
            SetNew(BuildingTypes.House, owner);
            maxPawnsE.Add();
        }
        public void SetNewSmelter(in PlayerTypes owner)
        {
            SetNew(BuildingTypes.Smelter, owner);
            IsActiveSmelterCRef.IsActive = false;
        }
        public void Destroy(in Entities ents)
        {
            if (BuildTCRef.Is(BuildingTypes.Teleport))
            {
                if (ents.StartTeleportE.Where == Idx) ents.StartTeleportE.Reset();
                else ents.EndTeleportE.Reset();
            }

            BuildTCRef.Build = BuildingTypes.None;
            PlayerTCRef.Player = PlayerTypes.None;
            HealthRef.Amount = 0;
        }
        public void Defrost(in Entities ents)
        {
            if (!HaveBuilding) throw new Exception("There's not got building on cell");
            if (!Is(BuildingTypes.IceWall)) throw new Exception("Need Ice Wall on cell");

            HealthRef.Amount--;
            if (!IsAlive) Destroy(ents);
        }
        public void Sync(in int health, in BuildingTypes build, in PlayerTypes player)
        {
            HealthRef.Amount = health;
            BuildTCRef.Build = build;
            PlayerTCRef.Player = player;
        }
        public void ToggleSmelter()
        {
            IsActiveSmelterCRef.IsActive = !IsActiveSmelterCRef.IsActive;
        }

        public void Build_Master(in byte idx_to_0, in BuildingTypes buildT, in Player sender, in Entities ents)
        {
            var idx_from = Idx;

            var whoseMove = ents.WhoseMoveE.WhoseMove.Player;


            foreach (var idx_to_1 in ents.CellSpaceWorker.GetIdxsAround(idx_to_0))
            {
                if (ents.BuildE(idx_to_1).Is(BuildingTypes.City, BuildingTypes.House, BuildingTypes.Market, BuildingTypes.Smelter))
                {
                    ents.CellSpaceWorker.TryGetDirect(idx_to_0, idx_to_1, out var dir);

                    if (dir == DirectTypes.Left || dir == DirectTypes.Right || dir == DirectTypes.Up || dir == DirectTypes.Down)
                    {
                        if (ents.InventorResourcesEs.CanBuyBuilding_Master(buildT, whoseMove, out var needRes))
                        {
                            ents.InventorResourcesEs.BuyBuilding_Master(buildT, whoseMove);

                            if (buildT == BuildingTypes.House)
                            {
                                ents.BuildE(idx_to_0).SetNewHouse(whoseMove, ents.MaxAvailablePawnsE(whoseMove));
                            }
                            else if (buildT == BuildingTypes.Smelter)
                            {
                                ents.BuildE(idx_to_0).SetNewSmelter(whoseMove);
                            }
                            else
                            {
                                ents.BuildE(idx_to_0).SetNew(buildT, whoseMove);
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