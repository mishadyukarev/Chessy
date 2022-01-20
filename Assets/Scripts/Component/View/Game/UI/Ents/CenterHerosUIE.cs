using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterHerosUIE
    {
        static Entity _parent;
        static Dictionary<UnitTypes, Entity> _ents;

        public static ref GameObjectVC Parent => ref _parent.Get<GameObjectVC>();
        public static ref ButtonUIC ButtonC(in UnitTypes unit) => ref _ents[unit].Get<ButtonUIC>();

        public CenterHerosUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _ents = new Dictionary<UnitTypes, Entity>();

            var parent = centerZone.transform.Find("HeroesZone");


            _parent = gameW.NewEntity()
                .Add(new GameObjectVC(parent.gameObject));

            for (var unit = UnitTypes.Elfemale; unit <= UnitTypes.Snowy; unit++)
            {
                _ents.Add(unit, gameW.NewEntity()
                    .Add(new ButtonUIC(parent.Find(unit.ToString()).Find("Button").GetComponent<Button>())));
            }




            _ents.Add(UnitTypes.None, gameW.NewEntity()
                .Add(new ButtonUIC(parent.Find("Premium_Button").GetComponent<Button>())));
        }
    }
}