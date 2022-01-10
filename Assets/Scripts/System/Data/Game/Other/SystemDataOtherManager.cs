using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class SystemDataOtherManager
    {
        readonly static Dictionary<string, System.Action> _systems;

        static SystemDataOtherManager()
        {
            _systems = new Dictionary<string, System.Action>();
        }
        public SystemDataOtherManager(in WorldEcs gameW)
        {
            _systems["0"] = new SyncOS().Run;
        }
    }
}