using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct CellsGiveTWComp
    {
        private Dictionary<PlayerTypes , Dictionary<ToolWeaponTypes, List<byte>>> _cellsGiveToolWeaps;

        internal CellsGiveTWComp(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsGiveToolWeaps = new Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, List<byte>>>();


                _cellsGiveToolWeaps.Add(PlayerTypes.First, new Dictionary<ToolWeaponTypes, List<byte>>());
                _cellsGiveToolWeaps.Add(PlayerTypes.Second, new Dictionary<ToolWeaponTypes, List<byte>>());


                _cellsGiveToolWeaps[PlayerTypes.First].Add(ToolWeaponTypes.Pick, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.First].Add(ToolWeaponTypes.Sword, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.First].Add(ToolWeaponTypes.Crossbow, new List<byte>());

                _cellsGiveToolWeaps[PlayerTypes.Second].Add(ToolWeaponTypes.Pick, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.Second].Add(ToolWeaponTypes.Sword, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.Second].Add(ToolWeaponTypes.Crossbow, new List<byte>());
            }
        }

        internal void Clear(PlayerTypes playerType, ToolWeaponTypes toolWeaponType) => _cellsGiveToolWeaps[playerType][toolWeaponType].Clear();
        internal void Add(PlayerTypes playerType, ToolWeaponTypes toolWeaponType,  byte idxCell) => _cellsGiveToolWeaps[playerType][toolWeaponType].Add(idxCell);
    }
}
