namespace Chessy.Game.System.Model
{
    sealed class WorldCountsAmountBuildingInGameS : CellSystem, IEcsRunSystem
    {
        readonly BuildingTypes _buildingT;
        readonly PlayerTypes _playerT;

        internal WorldCountsAmountBuildingInGameS(in BuildingTypes buildingT, in PlayerTypes playerT, in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
            _buildingT = buildingT;
            _playerT = playerT;
        }

        public void Run()
        {
            E.BuildingsInfo(_playerT, LevelTypes.First, _buildingT).IdxC.Clear();

            if (E.BuildingTC(Idx).Is(_buildingT) && E.BuildingPlayerTC(Idx).Is(_playerT))
            {
                E.BuildingsInfo(E.BuildingPlayerTC(Idx).Player, E.BuildingLevelTC(Idx).Level, E.BuildingTC(Idx).Building).IdxC.Add(Idx);
            } 
        }
    }
}