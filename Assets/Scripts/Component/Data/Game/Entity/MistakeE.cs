using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct MistakeE
    {
        static Entity _mistake;
        static Dictionary<ResourceTypes, Entity> _needRes;

        public static ref C Mistake<C>() where C : struct => ref _mistake.Get<C>();
        public static ref AmountC Mistake(in ResourceTypes res) => ref _needRes[res].Get<AmountC>();


        public static HashSet<ResourceTypes> Keys
        {
            get
            {
                var keys = new HashSet<ResourceTypes>();
                foreach (var item in _needRes) keys.Add(item.Key);
                return keys;
            }
        }

        public MistakeE(in EcsWorld gameW)
        {
            _needRes = new Dictionary<ResourceTypes, Entity>();

            _mistake = gameW.NewEntity()
                .Add(new MistakeC())
                .Add(new TimerC());

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                _needRes.Add(res, gameW.NewEntity()
                    .Add(new AmountC()));
        }
    }
}
