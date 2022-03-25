using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    public sealed class BuildBuildingClickS : SystemModelGameAbs
    {
        public BuildBuildingClickS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click(in BuildingTypes buildT)
        {
            var curPlayerI = eMGame.CurPlayerITC.Player;

            if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
            {
                if (eMGame.SelectedE.BuildingsC.Is(buildT))
                {
                    eMGame.SelectedE.BuildingsC.Set(buildT, false);
                    eMGame.Sound(ClipTypes.Click).Invoke();
                }
                else if (eMGame.PlayerInfoE(curPlayerI).HaveBuilding(buildT))
                {
                    eMGame.SelectedE.BuildingsC.Set(buildT, true);
                    eMGame.Sound(ClipTypes.Click).Invoke();
                }
                else
                {
                    eMGame.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                }
            }



            switch (buildT)
            {
                case BuildingTypes.House:
                    eMGame.RpcPoolEs.CityBuyBuildingToMaster(buildT);
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