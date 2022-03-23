﻿using System.Collections.Generic;

namespace Chessy.Game
{
    public struct PlayerLevelInfoE
    {
        readonly int[] _twTC;
        readonly PlayerLevelBuildingInfoE[] _buildingInfoEs;
        readonly Dictionary<UnitTypes, int> _unitsInGame;

        //public float WaterUnitMax;

        public ref int ToolWeapons(in ToolWeaponTypes tw) => ref _twTC[(byte)tw];
        public ref PlayerLevelBuildingInfoE BuildingInfoE(in BuildingTypes buildT) => ref _buildingInfoEs[(byte)buildT - 1];

        public int UnitsInGame(in UnitTypes unitT) => _unitsInGame[unitT];

        public PlayerLevelInfoE(in LevelTypes levT) : this()
        {
            _twTC = new int[(byte)ToolWeaponTypes.End];
            _buildingInfoEs = new PlayerLevelBuildingInfoE[(byte)BuildingTypes.End];

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingInfoEs[(byte)buildT - 1] = new PlayerLevelBuildingInfoE(default);
            }

            _unitsInGame = new Dictionary<UnitTypes, int>();

            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitsInGame.Add(unitT, 0);
            }
        }

        public void SetUnitsInGame(in UnitTypes unitT, in int units) => _unitsInGame[unitT] = units;
        public void Take(in UnitTypes unitT, in int taking) => _unitsInGame[unitT] -= taking;
        public void Add(in UnitTypes unitT, in int adding) => _unitsInGame[unitT] += adding;


        public void StartGame()
        {
            for (var i = 0; i < _twTC.Length; i++)
            {
                _twTC[i] = 0;         
            }

            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitsInGame[unitT] = 0;
            }

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingInfoEs[(byte)buildT - 1].IdxC.Clear();
            }
        }
    }
}