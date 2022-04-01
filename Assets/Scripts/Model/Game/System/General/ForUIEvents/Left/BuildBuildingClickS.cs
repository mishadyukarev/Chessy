using Chessy.Game.Entity.Model;
using System;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed class BuildBuildingClickS : SystemModelGameAbs
    {
        internal BuildBuildingClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click(in BuildingTypes buildT)
        {
            var curPlayerI = eMG.CurPlayerITC.PlayerT;

            if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
            {
                if (eMG.SelectedE.BuildingsC.Is(buildT))
                {
                    eMG.SelectedE.BuildingsC.Set(buildT, false);
                    eMC.SoundActionC(ClipCommonTypes.Click).Invoke();
                }
                else if (eMG.PlayerInfoE(curPlayerI).HaveBuilding(buildT))
                {
                    eMG.SelectedE.BuildingsC.Set(buildT, true);
                    eMC.SoundActionC(ClipCommonTypes.Click).Invoke();
                }
                else
                {
                    eMG.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                }
            }



            switch (buildT)
            {
                case BuildingTypes.House:
                    eMG.RpcPoolEs.CityBuyBuildingToMaster(buildT);
                    break;

                case BuildingTypes.Market:
                    break;

                case BuildingTypes.Smelter:
                    break;

                default: throw new Exception();
            }

            eMG.NeedUpdateView = true;
        }
    }
}