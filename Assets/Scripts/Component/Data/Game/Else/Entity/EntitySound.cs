using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntitySound
    {
        static Dictionary<ClipTypes, Entity> _sounds0;
        static Dictionary<UniqueAbilityTypes, Entity> _sounds1;

        public static ref ActionC Sound(in ClipTypes clip)
        {
            if (!_sounds0.ContainsKey(clip)) throw new Exception();
            return ref _sounds0[clip].Get<ActionC>();
        }
        public static ref ActionC Sound(in UniqueAbilityTypes clip)
        {
            if (!_sounds1.ContainsKey(clip)) throw new Exception();
            return ref _sounds1[clip].Get<ActionC>();
        }

        public EntitySound(in EcsWorld gameW, in Dictionary<ClipTypes, Action> action0, in Dictionary<UniqueAbilityTypes, Action> action1)
        {
            _sounds0 = new Dictionary<ClipTypes, Entity>();
            _sounds1 = new Dictionary<UniqueAbilityTypes, Entity>();

            foreach (var item in action0) _sounds0.Add(item.Key, gameW.NewEntity().Add(new ActionC(item.Value)));
            foreach (var item in action1) _sounds1.Add(item.Key, gameW.NewEntity().Add(new ActionC(item.Value)));
        }
    }
}