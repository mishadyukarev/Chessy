using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct AvailableCenterHeroEs
    {
        static Dictionary<PlayerTypes, Entity> _availHero;

        public static ref HaveC HaveAvailHero(in PlayerTypes player) => ref _availHero[player].Get<HaveC>();

        public AvailableCenterHeroEs(in EcsWorld gameW)
        {
            _availHero = new Dictionary<PlayerTypes, Entity>();

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _availHero.Add(player, gameW.NewEntity()
                    .Add(new HaveC(true)));
            }
        }
    }
}