using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{   
    public readonly struct EntityLeftCityUIPool
    {
        static Dictionary<LeftCityEntType, Entity> _ents;
        static Dictionary<ResourceTypes, Entity> _buyEnts;

        public static ref T Melt<T>() where T : struct, ILeftCityMeltButtonUIE => ref _ents[LeftCityEntType.Melt].Get<T>();
        public static ref T Resources<T>(in ResourceTypes res) where T : struct, ILeftCityBuyButtonsUIE => ref _buyEnts[res].Get<T>();


        public EntityLeftCityUIPool(in EcsWorld gameW, in Transform leftZone)
        {
            _ents = new Dictionary<LeftCityEntType, Entity>();
            _buyEnts = new Dictionary<ResourceTypes, Entity>();

            for (var res = ResourceTypes.None; res <= ResourceTypes.End; res++) _buyEnts.Add(res, default);


            var buildZone = leftZone.transform.Find("BuildingZone");

            _ents.Add(LeftCityEntType.Melt, gameW.NewEntity()
                .Add(new ButtonUIC(buildZone.Find("MeltOreButton").GetComponent<Button>())));

            _buyEnts[ResourceTypes.Food] = gameW.NewEntity().Add(new ButtonUIC(buildZone.Find("UpgradeFarm_Button").GetComponent<Button>()));
            _buyEnts[ResourceTypes.Wood] = gameW.NewEntity().Add(new ButtonUIC(buildZone.Find("UpgradeWoodcutter_Button").GetComponent<Button>()));
        }

        enum LeftCityEntType
        {
            None,
            Start = None,

            Melt,

            End,
        }
        public interface ILeftCityMeltButtonUIE { }
        public interface ILeftCityBuyButtonsUIE { }
    }
}