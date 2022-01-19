using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct MistakeE
    {
        static Entity _mistake;
        static readonly Dictionary<ResourceTypes, Entity> _needRes;

        public static ref C Mistake<C>() where C : struct => ref _mistake.Get<C>();
        public static ref C Mistake<C>(in ResourceTypes res) where C : struct => ref _needRes[res].Get<C>();


        public static HashSet<ResourceTypes> Keys
        {
            get
            {
                var keys = new HashSet<ResourceTypes>();
                foreach (var item in _needRes) keys.Add(item.Key);
                return keys;
            }
        }

        static MistakeE()
        {
            _needRes = new Dictionary<ResourceTypes, Entity>();
            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++) _needRes.Add(res, default);
        }
        public MistakeE(in EcsWorld gameW)
        {
            _mistake = gameW.NewEntity()
                .Add(new MistakeC())
                .Add(new TimerC());

            foreach (var key in Keys)
            {
                _needRes[key] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }
    }
}
