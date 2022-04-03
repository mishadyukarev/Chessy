using Chessy.Game.Model.Component;
using Chessy.Game.Model.Entity;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class PlayerInfoEs
    {
        readonly PlayerLevelInfoE[] _levelInfoEs;
        readonly ResourcesC[] _resourceCs;
        readonly Dictionary<UnitTypes, PlayerUnitInfoE> _unitEs;

        public bool IsReadyC;
        public float WoodForBuyHouse;

        public BuildingsInfoC BuildingsInfoC;
        public IdxsCellsC WhereKingEffects;

        public KingInfoE KingInfoE;
        public PawnInfoE PawnInfoE;
        public GodInfoE GodInfoE;


        public ref PlayerLevelInfoE LevelE(in LevelTypes levT) => ref _levelInfoEs[(byte)levT - 1];
        public ref ResourcesC ResourcesC(in ResourceTypes resT) => ref _resourceCs[(byte)resT - 1];
        public PlayerUnitInfoE UnitE(in UnitTypes unitT) => _unitEs[unitT];

        internal PlayerInfoEs(in bool b)
        {
            _levelInfoEs = new PlayerLevelInfoE[(byte)LevelTypes.End - 1];
            _resourceCs = new ResourcesC[(byte)ResourceTypes.End - 1];
            _unitEs = new Dictionary<UnitTypes, PlayerUnitInfoE>();
            WhereKingEffects = new IdxsCellsC(new HashSet<byte>());

            var haveBuilding = new Dictionary<BuildingTypes, bool>();
            haveBuilding.Add(BuildingTypes.Market, false);
            haveBuilding.Add(BuildingTypes.Smelter, false);
            BuildingsInfoC = new BuildingsInfoC(haveBuilding);


            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                _levelInfoEs[(byte)levT - 1] = new PlayerLevelInfoE(levT);
            }
            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitEs.Add(unitT, new PlayerUnitInfoE(unitT));
            }
        }
    }
}