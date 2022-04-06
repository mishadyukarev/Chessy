using Chessy.Common;
using Chessy.Common.Component;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct LeftUIEs
    {
        readonly Dictionary<BuildingTypes, LeftCityUIE> _cityButtonEs;

        public ButtonUIC PremiumButtonC;
        public GameObjectVC FutureGOC;
        public readonly LeftEnvironmentUIEs EnvironmentEs;


        public LeftCityUIE CityE(in BuildingTypes buildingT) => _cityButtonEs[buildingT];


        internal LeftUIEs(in Transform leftZone)
        {
            _cityButtonEs = new Dictionary<BuildingTypes, LeftCityUIE>();
            var cityZone = leftZone.transform.Find("City+");


            PremiumButtonC = new ButtonUIC(cityZone.Find("Premium_Button").GetComponent<Button>());
            FutureGOC = new GameObjectVC(cityZone.Find("Future+").gameObject);

            for (var buildingT = BuildingTypes.House; buildingT <= BuildingTypes.Smelter; buildingT++)
            {
                _cityButtonEs.Add(buildingT, new LeftCityUIE(cityZone.Find(buildingT + "+")));
            }

            EnvironmentEs = new LeftEnvironmentUIEs(leftZone);
        }
    }
}