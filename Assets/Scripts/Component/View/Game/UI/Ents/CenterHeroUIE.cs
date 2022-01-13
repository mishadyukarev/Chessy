using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterHeroUIE
    {
        static Dictionary<UnitTypes, Entity> _ents;

        public static ref C Unit<C>(in UnitTypes unit) where C : struct => ref _ents[unit].Get<C>();

        public CenterHeroUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _ents = new Dictionary<UnitTypes, Entity>();

            var parent = centerZone.transform.Find("HeroesZone");

            _ents.Add(UnitTypes.Elfemale, gameW.NewEntity()
                .Add(new ButtonUIC(parent.Find("Elffemale_But").GetComponent<Button>())));

            _ents.Add(UnitTypes.None, gameW.NewEntity()
                .Add(new ButtonUIC(parent.Find("Premium_Button").GetComponent<Button>())));
        }
    }
}