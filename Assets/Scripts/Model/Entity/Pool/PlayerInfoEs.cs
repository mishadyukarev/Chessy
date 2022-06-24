using Chessy.Model.Model.Component;
using Chessy.Model.Model.Entity;
using System.Collections.Generic;

namespace Chessy.Model
{
    public struct PlayerInfoEs
    {
        readonly PlayerLevelInfoE[] _levelInfoEs;
        readonly ResourcesC[] _resourceCs;
        readonly PlayerUnitInfoE[] _unitEs;

        public bool IsReadyForStartOnlineGame;
        public float WoodForBuyHouse;
        public int AmountFarmsInGame;

        public BuildingsInTownInfoC BuildingsInfoC;
        public PawnPeopleInfoC PawnInfoC;

        public KingInfoE KingInfoE;
        public GodInfoE GodInfoE;


        public ref PlayerLevelInfoE LevelE(in LevelTypes levT) => ref _levelInfoEs[(byte)levT];
        public ref ResourcesC ResourcesC(in ResourceTypes resT) => ref _resourceCs[(byte)resT];
        public PlayerUnitInfoE UnitE(in UnitTypes unitT) => _unitEs[(byte)unitT];

        internal PlayerInfoEs(in bool def) : this()
        {
            _levelInfoEs = new PlayerLevelInfoE[(byte)LevelTypes.End];
            _resourceCs = new ResourcesC[(byte)ResourceTypes.End];
            _unitEs = new PlayerUnitInfoE[(byte)UnitTypes.End];

            var haveBuilding = new bool[(byte)BuildingTypes.End];
            BuildingsInfoC = new BuildingsInTownInfoC(haveBuilding);


            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                _levelInfoEs[(byte)levT] = new PlayerLevelInfoE(levT);
            }
            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitEs[(byte)unitT] = new PlayerUnitInfoE(unitT);
            }
        }
    }
}