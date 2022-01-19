using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct BuildingUpgradesEs
    {
        static Dictionary<string, Entity> _ents;


        static string Key(in BuildingTypes build, in PlayerTypes player, in UpgradeTypes upg) => build.ToString() + player + upg;

        public static ref C HaveUpgrade<C>(in BuildingTypes build, in PlayerTypes player, in UpgradeTypes upg) where C : struct => ref _ents[Key(build, player, upg)].Get<C>();

        public BuildingUpgradesEs(in EcsWorld gameW)
        {
            _ents = new Dictionary<string, Entity>();

            for (var build = BuildingTypes.Start + 1; build < BuildingTypes.End; build++)
            {
                for (var player = PlayerTypes.Start + 1; player < PlayerTypes.End; player++)
                {
                    for (var upg = UpgradeTypes.None + 1; upg < UpgradeTypes.End; upg++)
                    {
                        _ents.Add(Key(build, player, upg), gameW.NewEntity()
                            .Add(new HaveUpgradeC(false)));
                    }
                }
            }
        }
    }
}