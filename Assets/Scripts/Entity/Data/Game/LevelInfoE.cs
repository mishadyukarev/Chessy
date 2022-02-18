using System.Collections.Generic;

namespace Game.Game
{
    public struct LevelInfoE
    {
        readonly AmountC[] _twTC;
        readonly IdxsC[] _buildsInGame;

        public ref AmountC ToolWeapons(in ToolWeaponTypes tw) => ref _twTC[(byte)tw];
        public ref IdxsC BuildsInGame(in BuildingTypes buildT) => ref _buildsInGame[(byte)buildT];

        public LevelInfoE(in bool def)
        {
            _twTC = new AmountC[(byte)ToolWeaponTypes.End];
            _buildsInGame = new IdxsC[(byte)BuildingTypes.End];

            for (var buildT = BuildingTypes.None; buildT < BuildingTypes.End; buildT++)
            {
                _buildsInGame[(byte)buildT] = new IdxsC(new HashSet<byte>());
            }
        }
    }
}