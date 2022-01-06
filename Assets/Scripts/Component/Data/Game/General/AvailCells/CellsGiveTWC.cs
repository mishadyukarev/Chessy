using System.Collections.Generic;

namespace Game.Game
{
    public struct CellsGiveTWC
    {
        private Dictionary<PlayerTypes, Dictionary<TWTypes, List<byte>>> _cellsGiveToolWeaps;

        public CellsGiveTWC(bool needNew) : this()
        {
            if (needNew)
            {
                _cellsGiveToolWeaps = new Dictionary<PlayerTypes, Dictionary<TWTypes, List<byte>>>();


                _cellsGiveToolWeaps.Add(PlayerTypes.First, new Dictionary<TWTypes, List<byte>>());
                _cellsGiveToolWeaps.Add(PlayerTypes.Second, new Dictionary<TWTypes, List<byte>>());


                _cellsGiveToolWeaps[PlayerTypes.First].Add(TWTypes.Pick, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.First].Add(TWTypes.Sword, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.First].Add(TWTypes.Shield, new List<byte>());

                _cellsGiveToolWeaps[PlayerTypes.Second].Add(TWTypes.Pick, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.Second].Add(TWTypes.Sword, new List<byte>());
                _cellsGiveToolWeaps[PlayerTypes.Second].Add(TWTypes.Shield, new List<byte>());
            }
        }

        public void Clear(PlayerTypes playerType, TWTypes toolWeaponType) => _cellsGiveToolWeaps[playerType][toolWeaponType].Clear();
        public void Add(PlayerTypes playerType, TWTypes toolWeaponType, byte idxCell) => _cellsGiveToolWeaps[playerType][toolWeaponType].Add(idxCell);
    }
}
