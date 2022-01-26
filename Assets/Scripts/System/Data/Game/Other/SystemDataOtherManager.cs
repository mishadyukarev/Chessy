using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemDataOtherManager
    {
        static Dictionary<string, System.Action> _systems;

        public SystemDataOtherManager(in bool def)
        {
            _systems = new Dictionary<string, System.Action>();
        }
    }
}