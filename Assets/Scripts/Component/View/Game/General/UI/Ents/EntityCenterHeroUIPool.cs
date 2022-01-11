using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityCenterHeroUIPool
    {
        static readonly Dictionary<UnitTypes, Entity> _ents;

        public static ref C Unit<C>(in UnitTypes unit) where C : struct => ref _ents[unit].Get<C>();

        static EntityCenterHeroUIPool()
        {
            _ents = new Dictionary<UnitTypes, Entity>();

            for (var unit = UnitTypes.Start; unit <= UnitTypes.End; unit++) _ents.Add(unit, default);
        }
        public EntityCenterHeroUIPool(in WorldEcs gameW, in Transform centerZone)
        {
            var parent = centerZone.transform.Find("HeroesZone");

            _ents[UnitTypes.Elfemale] = gameW.NewEntity().Add(new ButtonVC(parent.Find("Elffemale_But").GetComponent<Button>()));
            _ents[UnitTypes.None] = gameW.NewEntity().Add(new ButtonVC(parent.Find("Premium_Button").GetComponent<Button>()));
        }
    }
}