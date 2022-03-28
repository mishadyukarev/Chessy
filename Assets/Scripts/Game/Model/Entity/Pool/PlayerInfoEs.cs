using Chessy.Common;
using Chessy.Common.Interface;
using Chessy.Game.Enum;
using Chessy.Game.Model.Component;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class PlayerInfoEs : IToggleScene
    {
        readonly Dictionary<BuildingTypes, bool> _haveBuilding;
        readonly PlayerLevelInfoE[] _levelInfoEs;
        readonly ResourcesC[] _resourceCs;
        readonly Dictionary<UnitTypes, PlayerUnitInfoE> _unitEs;


        public bool IsReadyC;
        public float PeopleInCity;
        public int MaxAvailablePawns;
        public float WoodForBuyHouse;
        public bool HaveHeroInInventor;

        public bool HaveKingInInventor;
        public byte KingCell;


        public UnitTC MyHeroTC;
        public CooldownC HeroCooldownC;
        public IdxsCellsC WhereKingEffects;


        public ref PlayerLevelInfoE LevelE(in LevelTypes levT) => ref _levelInfoEs[(byte)levT - 1];
        public ref ResourcesC ResourcesC(in ResourceTypes resT) => ref _resourceCs[(byte)resT - 1];
        public PlayerUnitInfoE UnitE(in UnitTypes unitT) => _unitEs[unitT];

        public bool HaveBuilding(in BuildingTypes buildingT) => _haveBuilding[buildingT];


        public PlayerInfoEs(in bool b)
        {
            _levelInfoEs = new PlayerLevelInfoE[(byte)LevelTypes.End - 1];
            _resourceCs = new ResourcesC[(byte)ResourceTypes.End - 1];
            _unitEs = new Dictionary<UnitTypes, PlayerUnitInfoE>();
            _haveBuilding = new Dictionary<BuildingTypes, bool>();
            _haveBuilding.Add(BuildingTypes.Market, false);
            _haveBuilding.Add(BuildingTypes.Smelter, false);
            WhereKingEffects = new IdxsCellsC(new HashSet<byte>());

            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                _levelInfoEs[(byte)levT - 1] = new PlayerLevelInfoE(levT);
            }
            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitEs.Add(unitT, new PlayerUnitInfoE(unitT));
            }
        }

        public void SetHaveBuilding(in BuildingTypes buildingT, in bool have) => _haveBuilding[buildingT] = have;


        public void ToggleScene(in SceneTypes newSceneT)
        {
            PeopleInCity = StartValues.PEOPLE_IN_CITY;
            MaxAvailablePawns = StartValues.MAX_AVAILABLE_PAWN;
            HaveKingInInventor = true;
            HaveHeroInInventor = true;
            WoodForBuyHouse = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;
            IsReadyC = false;


            MyHeroTC.Unit = UnitTypes.None;
            HeroCooldownC.Cooldown = 0;
            WhereKingEffects.Clear();

            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                _levelInfoEs[(byte)levT - 1].StartGame();
            }
        }
    }
}