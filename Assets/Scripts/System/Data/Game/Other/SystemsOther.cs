using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemsOther
    {
        readonly Dictionary<string, System.Action> _systems;

        public SystemsOther(in Entities ents)
        {
            _systems = new Dictionary<string, System.Action>();
        }
    }
}