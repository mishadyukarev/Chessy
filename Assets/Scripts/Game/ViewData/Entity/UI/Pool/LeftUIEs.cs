using Chessy.Common;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct LeftUIEs
    {
        readonly Dictionary<BuildingTypes, LeftCityUIE> _cityButtonEs;
        public readonly LeftEnvironmentUIEs EnvironmentEs;

        public LeftCityUIE CityE(in BuildingTypes buildingT) => _cityButtonEs[buildingT];

        internal LeftUIEs(in bool def)
        {
            var leftZone = CanvasC.FindUnderCurZone("Left+").transform;


            _cityButtonEs = new Dictionary<BuildingTypes, LeftCityUIE>();
            var cityZone = leftZone.transform.Find("City+");
            for (var buildingT = BuildingTypes.House; buildingT <= BuildingTypes.Smelter; buildingT++)
            {
                _cityButtonEs.Add(buildingT, new LeftCityUIE(cityZone.Find(buildingT + "+")));
            }

            EnvironmentEs = new LeftEnvironmentUIEs(leftZone);
        }
    }
}