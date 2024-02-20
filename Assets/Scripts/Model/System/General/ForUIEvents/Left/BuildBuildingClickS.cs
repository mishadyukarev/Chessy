using Photon.Pun;
using System;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ClickOntoTownBuilding(in BuildingTypes buildT)
        {
            if (buildT == BuildingTypes.Market || buildT == BuildingTypes.Smelter)
            {
                if (selectedBuildingsInTownC.Is(buildT))
                {
                    selectedBuildingsInTownC.Set(buildT, false);
                    dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
                }
                else if (buildingsInTownInfoCs[(byte)aboutGameC.CurrentPlayerIT].HaveBuilding(buildT))
                {
                    selectedBuildingsInTownC.Set(buildT, true);
                    dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
                }
                else
                {
                    rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryBuyBuildingInTownM), buildT });
                }
            }



            switch (buildT)
            {
                case BuildingTypes.House:
                    rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryBuyBuildingInTownM), buildT });
                    break;

                case BuildingTypes.Market:
                    //if (_eMG.LessonT == LessonTypes.ClickBuyMarketInTown)
                    //{
                    //    _eMG.LessonTC.SetNextLesson();
                    //    _eMG.AdultForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = ValuesChessy.MAX_RESOURCES;
                    //    _eMG.BuildingTC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).BuildingT = BuildingTypes.None;
                    //}
                    break;

                case BuildingTypes.Smelter:
                    break;

                default: throw new Exception();
            }

            updateAllViewC.NeedUpdateView = true;
        }
    }
}