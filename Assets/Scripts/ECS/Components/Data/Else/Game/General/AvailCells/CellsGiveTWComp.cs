using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells
{
    internal struct CellsGiveTWComp
    {
        private Dictionary<ToolWeaponTypes, Dictionary<bool, List<byte>>> _cellsGiveToolWeaps;

        internal CellsGiveTWComp(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsGiveToolWeaps = new Dictionary<ToolWeaponTypes, Dictionary<bool, List<byte>>>();


                _cellsGiveToolWeaps.Add(ToolWeaponTypes.Pick, new Dictionary<bool, List<byte>>());
                _cellsGiveToolWeaps.Add(ToolWeaponTypes.Sword, new Dictionary<bool, List<byte>>());
                _cellsGiveToolWeaps.Add(ToolWeaponTypes.Crossbow, new Dictionary<bool, List<byte>>());


                _cellsGiveToolWeaps[ToolWeaponTypes.Pick].Add(true, new List<byte>());
                _cellsGiveToolWeaps[ToolWeaponTypes.Pick].Add(false, new List<byte>());

                _cellsGiveToolWeaps[ToolWeaponTypes.Sword].Add(true, new List<byte>());
                _cellsGiveToolWeaps[ToolWeaponTypes.Sword].Add(false, new List<byte>());

                _cellsGiveToolWeaps[ToolWeaponTypes.Crossbow].Add(true, new List<byte>());
                _cellsGiveToolWeaps[ToolWeaponTypes.Crossbow].Add(false, new List<byte>());
            }
        }

        internal void Clear(ToolWeaponTypes toolWeaponType, bool isMasterKey) => _cellsGiveToolWeaps[toolWeaponType][isMasterKey].Clear();
        internal void Add(ToolWeaponTypes toolWeaponType, bool isMasterKey, byte idxCell) => _cellsGiveToolWeaps[toolWeaponType][isMasterKey].Add(idxCell);
    }
}
