using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CurrentDirectWindE
    {
        static Entity _ent;

        public static ref C Direct<C>() where C : struct, IDirectWindE => ref _ent.Get<C>();

        public CurrentDirectWindE(in EcsWorld gameW)
        {
            _ent = gameW.NewEntity()
                .Add(new DirectTC(DirectTypes.Right));
        }
    }

    public interface IDirectWindE { }
}