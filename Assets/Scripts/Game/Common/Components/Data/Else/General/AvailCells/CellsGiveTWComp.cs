using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellsGiveTWComp
    {
        private Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, List<byte>>> _cellsGiveToolWeaps;

        public CellsGiveTWComp(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsGiveToolWeaps = new Dictionary<PlayerTypes, Dictionary<ToolWeaponTypes, List<byte>>>();


                _cellsGiveToolWeaps.Add(PlayerTypes.First, new Dictionary<ToolWeaponTypes, List<byte>>());
                _cellsGiveToolWeaps.Add(PlayerTypes.Second, new Dictionary<ToolWeaponTypes, List<byte>>());


                _cellsGiveToolWeaps[PlayerTypes.First].Add(ToolWeaponTypes.Pick, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.First].Add(ToolWeaponTypes.Sword, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.First].Add(ToolWeaponTypes.Shield, new List<byte>());

                _cellsGiveToolWeaps[PlayerTypes.Second].Add(ToolWeaponTypes.Pick, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.Second].Add(ToolWeaponTypes.Sword, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.Second].Add(ToolWeaponTypes.Shield, new List<byte>());
            }
        }

        public void Clear(PlayerTypes playerType, ToolWeaponTypes toolWeaponType) => _cellsGiveToolWeaps[playerType][toolWeaponType].Clear();
        public void Add(PlayerTypes playerType, ToolWeaponTypes toolWeaponType, byte idxCell) => _cellsGiveToolWeaps[playerType][toolWeaponType].Add(idxCell);
    }
}
