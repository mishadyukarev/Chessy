using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WindEnt
    {
        static Entity _ent;
        static Dictionary<DirectTypes, Entity> _directs;

        public static HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _directs) keys.Add(item.Key);
                return keys;
            }
        }

        static WindEnt()
        {
            _directs = new Dictionary<DirectTypes, Entity>();
            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                _directs.Add(dir, default);
            }
        }

        public WindEnt(in EcsWorld gameW)
        {
            _ent = gameW.NewEntity()
                .Add(new DirectC(DirectTypes.Right));

            foreach (var key in Keys)
            {
                _directs[key] = gameW.NewEntity()
                    .Add(new IdxC());
            }
        }
    }
}