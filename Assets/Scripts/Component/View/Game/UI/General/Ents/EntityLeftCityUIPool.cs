using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{   
    public readonly struct EntityLeftCityUIPool
    {
        static readonly Dictionary<LeftCityEntType, Entity> _ents;
        static readonly Dictionary<ResTypes, Entity> _buyEnts;

        public static ref T Melt<T>() where T : struct, ILeftCityMeltButtonUIE => ref _ents[LeftCityEntType.Melt].Get<T>();
        public static ref T Resources<T>(in ResTypes res) where T : struct, ILeftCityBuyButtonsUIE => ref _buyEnts[res].Get<T>();


        static EntityLeftCityUIPool()
        {
            _ents = new Dictionary<LeftCityEntType, Entity>();
            _buyEnts = new Dictionary<ResTypes, Entity>();

            for (var res = ResTypes.Start; res <= ResTypes.End; res++) _buyEnts.Add(res, default);
        }
        public EntityLeftCityUIPool(in EcsWorld gameW, in Transform leftZone)
        {
            var buildZone = leftZone.transform.Find("BuildingZone");

            _ents.Add(LeftCityEntType.Melt, gameW.NewEntity()
                .Add(new ButtonUIC(buildZone.Find("MeltOreButton").GetComponent<Button>())));

            _buyEnts[ResTypes.Food] = gameW.NewEntity().Add(new ButtonUIC(buildZone.Find("UpgradeFarm_Button").GetComponent<Button>()));
            _buyEnts[ResTypes.Wood] = gameW.NewEntity().Add(new ButtonUIC(buildZone.Find("UpgradeWoodcutter_Button").GetComponent<Button>()));
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