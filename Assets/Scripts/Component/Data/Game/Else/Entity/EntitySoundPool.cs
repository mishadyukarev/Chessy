using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntitySoundPool
    {
        readonly static Dictionary<ClipTypes, Entity> _sounds0;
        readonly static Dictionary<UniqueAbilityTypes, Entity> _sounds1;

        public static ref C Sound<C>(ClipTypes clip) where C : struct
        {
            if (!_sounds0.ContainsKey(clip)) throw new Exception();
            return ref _sounds0[clip].Get<C>();
        }
        public static ref C Sound<C>(UniqueAbilityTypes clip) where C : struct
        {
            if (!_sounds1.ContainsKey(clip)) throw new Exception();
            return ref _sounds1[clip].Get<C>();
        }

        static EntitySoundPool()
        {
            _sounds0 = new Dictionary<ClipTypes, Entity>();
            _sounds1 = new Dictionary<UniqueAbilityTypes, Entity>();
        }
        public EntitySoundPool(in WorldEcs gameW, in Dictionary<ClipTypes, Action> action0, in Dictionary<UniqueAbilityTypes, Action> action1)
        {
            foreach (var item in action0) _sounds0.Add(item.Key, gameW.NewEntity().Add(new ActionC(item.Value)));
            foreach (var item in action1) _sounds1.Add(item.Key, gameW.NewEntity().Add(new ActionC(item.Value)));
        }
    }
}