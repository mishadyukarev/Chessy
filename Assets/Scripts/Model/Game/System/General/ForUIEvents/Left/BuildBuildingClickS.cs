using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Model.System;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    public sealed class BuildBuildingClickS : SystemModel
    {
        internal BuildBuildingClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click(in BuildingTypes buildT)
        {



            if(eMG.CurPlayerIT == eMG.WhoseMovePlayerT)
            {
                if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
                {
                    if (eMG.SelectedE.BuildingsC.Is(buildT))
                    {
                        eMG.SelectedE.BuildingsC.Set(buildT, false);
                        eMC.SoundActionC(ClipCommonTypes.Click).Invoke();
                    }
                    else if (eMG.PlayerInfoE(eMG.CurPlayerIT).BuildingsInfoC.HaveBuilding(buildT))
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
                        if (eMG.LessonTC.Is(LessonTypes.ClickBuyMarketInTown))
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                        break;

                    case BuildingTypes.Smelter:
                        break;

                    default: throw new Exception();
                }

                
            }
            else
            {
                sMG.MistakeS.Mistake(MistakeTypes.NeedWaitQueue);
            }

            eMG.NeedUpdateView = true;
        }
    }
}