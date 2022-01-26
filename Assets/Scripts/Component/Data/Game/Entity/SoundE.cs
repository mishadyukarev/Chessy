using ECS;
using System;

namespace Game.Game
{
    public sealed class SoundE : EntityAbstract
    {
        public ref ActionC Sound => ref Ent.Get<ActionC>();

        public SoundE(in Action action, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new ActionC(action));
        }
    }
}