using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EntMistakeC
    {
        static Entity _mistake;
        static readonly Dictionary<ResTypes, Entity> _needRes;

        public static ref C Mistake<C>() where C : struct => ref _mistake.Get<C>();
        public static ref C Mistake<C>(in ResTypes res) where C : struct => ref _needRes[res].Get<C>();


        public static HashSet<ResTypes> Keys
        {
            get
            {
                var keys = new HashSet<ResTypes>();
                foreach (var item in _needRes) keys.Add(item.Key);
                return keys;
            }
        }

        static EntMistakeC()
        {
            _needRes = new Dictionary<ResTypes, Entity>();
            for (var res = ResTypes.First; res < ResTypes.End; res++) _needRes.Add(res, default);
        }
        public EntMistakeC(in EcsWorld gameW)
        {
            _mistake = gameW.NewEntity()
                .Add(new MistakeC())
                .Add(new TimerC());

            foreach (var key in Keys)
            {
                _needRes[key] = gameW.NewEntity()
                    .Add(new ResourceTypeC());
            }
        }
    }
}
