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

        public void Set(in BuildingTypes buildT, in LevelTypes levT, in float hp, PlayerTypes playerT)
        {
            BuildingC.Build = buildT;
            HealthC.Health = hp;
            PlayerC.Player = playerT;
            LevelTC.Level = levT;
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
    }
}