using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct AvailableCenterHeroEs
    {
        static Dictionary<PlayerTypes, Entity> _availHero;

        public static ref C HaveAvailHero<C>(in PlayerTypes player) where C : struct, IAvailableHeroE => ref _availHero[player].Get<C>();

        public AvailableCenterHeroEs(in EcsWorld gameW)
        {
            _availHero = new Dictionary<PlayerTypes, Entity>();

            for (var player = PlayerTypes.Start + 1; player < PlayerTypes.End; player++)
            {
                _availHero.Add(player, gameW.NewEntity()
                    .Add(new HaveAvailableHeroC(true)));
            }
        }
    }

    public interface IAvailableHeroE { }
}