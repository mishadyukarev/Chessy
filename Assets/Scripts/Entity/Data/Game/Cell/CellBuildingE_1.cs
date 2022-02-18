using Photon.Realtime;

namespace Game.Game
{
    public struct CellBuildingE
    {
        readonly bool[] _owners;
        public bool IsActiveSmelter;

        public BuildingTC BuildingC;
        public HealthC HealthC;
        public PlayerTC PlayerC;
        public LevelTC LevelTC;

        public ref bool IsVisible(in PlayerTypes player) => ref _owners[(byte)player - 1];


        public CellBuildingE(in byte types) : this()
        {
            _owners = new bool[types];
        }


        //public bool CanExtractFertilizer(in CellEnvironmentEs envEs)
        //{

        //    //if (HaveBuilding && BuildingC.Is(BuildingTypes.Farm)
        //    //    && envEs.FertilizeC.HaveAny) return true;
        //    //else return false;
        //}

        //public void SetNew(in BuildingTypes buildT, in PlayerTypes ownerT)
        //{
        //    if (HaveBuilding) throw new Exception("There's got building on cell");

        //    BuildingC.Build = buildT;
        //    PlayerC.Player = ownerT;
        //    HealthC.Health = CellBuildingValues.MaxAmountHealth(buildT);
        //}
        //public void SetNewHouse(in PlayerTypes owner, in MaxAvailablePawnsE maxPawnsE)
        //{
        //    SetNew(BuildingTypes.House, owner);
        //    maxPawnsE.Add();
        //}
        //public void SetNewSmelter(in PlayerTypes owner)
        //{
        //    SetNew(BuildingTypes.Smelter, owner);
        //    IsActiveSmelterC.IsActive = false;
        //}

        //public void Destroy(in Entities ents)
        //{
        //    if (BuildingC.Is(BuildingTypes.Teleport))
        //    {
        //        if (ents.StartTeleportIdxC == CellEs.Idx)
        //        {
        //            ents.StartTeleportIdxC = 0;
        //        }
        //        else ents.EndTeleportIdxC.Idx = 0;
        //    }

        //    BuildingC.Build = BuildingTypes.None;
        //    PlayerC.Player = PlayerTypes.None;
        //    HealthC.Health = 0;
        //}

        public void Build_Master(in byte idx_to_0, in BuildingTypes buildT, in Player sender, in Entities ents)
        {
            var whoseMove = ents.WhoseMove.Player;


            //foreach (var idx_to_1 in ents.CellSpaceWorker.GetIdxsAround(idx_to_0))
            //{
            //    if (ents.BuildTC(idx_to_1).Is(BuildingTypes.City, BuildingTypes.House, BuildingTypes.Market, BuildingTypes.Smelter))
            //    {
            //        ents.CellSpaceWorker.TryGetDirect(idx_to_0, idx_to_1, out var dir);

            //        if (dir == DirectTypes.Left || dir == DirectTypes.Right || dir == DirectTypes.Up || dir == DirectTypes.Down)
            //        {
            //            if (ents.InventorResourcesEs.CanBuyBuilding_Master(buildT, whoseMove, out var needRes))
            //            {
            //                ents.InventorResourcesEs.BuyBuilding_Master(buildT, whoseMove);

            //                if (buildT == BuildingTypes.House)
            //                {
            //                    //ents.BuildingE(idx_to_0).SetNewHouse(whoseMove, ents.MaxAvailablePawnsE(whoseMove));
            //                }
            //                else if (buildT == BuildingTypes.Smelter)
            //                {
            //                    //ents.BuildingE(idx_to_0).SetNewSmelter(whoseMove);
            //                }
            //                else
            //                {
            //                    //ents.BuildingE(idx_to_0).SetNew(buildT, whoseMove);
            //                }



            //                break;
            //            }
            //            else
            //            {
            //                ents.RpcE.MistakeEconomyToGeneral(sender, needRes);
            //            }
            //        }
            //    }
            //}
        }
    }
}