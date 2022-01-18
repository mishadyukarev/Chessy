using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct InventorToolWeaponE
    {
        static Dictionary<string, Entity> _tWs;

        static string Key(in ToolWeaponTypes tw, in LevelTypes level, in PlayerTypes player) => tw.ToString() + level + player;

        public static ref C ToolWeapons<C>(in ToolWeaponTypes tw, in LevelTypes level, in PlayerTypes player) where C : struct => ref _tWs[Key(tw, level, player)].Get<C>();
        public static ref C ToolWeapons<C>(in string key) where C : struct => ref _tWs[key].Get<C>();

        public static HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _tWs) keys.Add(item.Key);
                return keys;
            }
        }

        static InventorToolWeaponE()
        {
            _tWs = new Dictionary<string, Entity>();

            for (var tw = ToolWeaponTypes.Pick; tw < ToolWeaponTypes.End; tw++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                    {
                        _tWs.Add(Key(tw, level, player), default);
                    }
                }
            }
        }
        public InventorToolWeaponE(in EcsWorld gameW)
        {
            foreach (var key in Keys)
            {
                _tWs[key] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }
    }
}
