using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InventorToolWeaponEs
    {
        readonly Dictionary<string, ToolWeaponInInventor> _tWs;

        string Key(in ToolWeaponTypes tw, in LevelTypes level, in PlayerTypes player) => tw.ToString() + level + player;

        public ToolWeaponInInventor ToolWeapons(in ToolWeaponTypes tw, in LevelTypes level, in PlayerTypes player) => _tWs[Key(tw, level, player)];
        public ToolWeaponInInventor ToolWeapons(in string key) => _tWs[key];

        public HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _tWs) keys.Add(item.Key);
                return keys;
            }
        }

        public InventorToolWeaponEs(in EcsWorld gameW)
        {
            _tWs = new Dictionary<string, ToolWeaponInInventor>();

            for (var tw = ToolWeaponTypes.None + 1; tw < ToolWeaponTypes.End; tw++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                    {
                        _tWs.Add(Key(tw, level, player), new ToolWeaponInInventor(0, gameW));
                    }
                }
            }
        }
    }
}
