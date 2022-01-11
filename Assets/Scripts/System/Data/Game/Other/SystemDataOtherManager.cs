using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemDataOtherManager
    {
        readonly static Dictionary<string, System.Action> _systems;

        static SystemDataOtherManager()
        {
            _systems = new Dictionary<string, System.Action>();
        }
        public SystemDataOtherManager(in bool def)
        {

        }
    }
}