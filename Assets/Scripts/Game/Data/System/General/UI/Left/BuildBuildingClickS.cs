using System;

namespace Chessy.Game.System.Model
{
    public struct BuildBuildingClickS
    {
        public void Click(in BuildingTypes buildT, in EntitiesModel e)
        {
            var curPlayerI = e.CurPlayerITC.Player;

            if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
            {
                if (e.SelectedE.BuildingsC.Is(buildT))
                {
                    e.SelectedE.BuildingsC.Set(buildT, false);
                    e.Sound(ClipTypes.Click).Invoke();
                }
                else if (e.PlayerInfoE(curPlayerI).HaveBuilding(buildT))
                {
                    e.SelectedE.BuildingsC.Set(buildT, true);
                    e.Sound(ClipTypes.Click).Invoke();
                }
                else
                {
                    e.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                }
            }



            switch (buildT)
            {
                case BuildingTypes.House:
                    e.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                    break;

                case BuildingTypes.Market:
                    break;

                case BuildingTypes.Smelter:
                    break;

                default: throw new Exception();
            }
        }
    }
}