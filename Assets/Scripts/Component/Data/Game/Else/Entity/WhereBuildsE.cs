using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WhereBuildsE
    {
        static Dictionary<string, Entity> _builds;

        static string Key(in BuildTypes build, in PlayerTypes owner, in byte idx) => build.ToString() + owner + idx;

        public static ref C HaveBuild<C>(in BuildTypes build, in PlayerTypes owner, in byte idx) where C : struct, IWhereBuildsE => ref _builds[Key(build, owner, idx)].Get<C>();
        public static ref C HaveBuild<C>(in string key) where C : struct, IWhereBuildsE => ref _builds[key].Get<C>();


        public static HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _builds) hash.Add(item.Key);
                return hash;
            }
        }

        public static bool IsSetted(in BuildTypes build, in PlayerTypes owner, out byte idx)
        {
            for (idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (HaveBuild<HaveBuildingC>(build, owner, idx).Have)
                {
                    return true;
                }
            }
            return false;
        }

        public WhereBuildsE(in EcsWorld gameW)
        {
            _builds = new Dictionary<string, Entity>();

            for (var build = BuildTypes.First; build < BuildTypes.End; build++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
                    {
                        _builds.Add(Key(build, player, idx), gameW.NewEntity()
                            .Add(new HaveBuildingC()));
                    }
                }
            }
        }
    }

    public interface IWhereBuildsE { }
}
