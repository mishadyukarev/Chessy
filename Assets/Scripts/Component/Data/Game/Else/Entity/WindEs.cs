using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WindEs
    {
        static Entity _ent;
        static Dictionary<DirectTypes, Entity> _directs;


        public static ref C Wind<C>() where C : struct, IDirectWindE => ref _ent.Get<C>();

        public static HashSet<DirectTypes> Keys
        {
            get
            {
                var keys = new HashSet<DirectTypes>();
                foreach (var item in _directs) keys.Add(item.Key);
                return keys;
            }
        }

        public WindEs(in EcsWorld gameW)
        {
            _directs = new Dictionary<DirectTypes, Entity>();
            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                _directs.Add(dir, gameW.NewEntity()
                    .Add(new IdxC()));
            }

            _ent = gameW.NewEntity()
                .Add(new DirectTC(DirectTypes.Right));
        }
    }

    public interface IDirectWindE { }
}