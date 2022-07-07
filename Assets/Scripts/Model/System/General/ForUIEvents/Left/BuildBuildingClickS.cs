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
                if (_e.SelectedE.BuildingsC.Is(buildT))
                {
                    _e.SelectedE.BuildingsC.Set(buildT, false);
                    _e.SoundAction(ClipTypes.Click).Invoke();
                }
                else if (_e.PlayerInfoE(_e.CurrentPlayerIT).BuildingsInTownInfoC.HaveBuilding(buildT))
                {
                    _e.SelectedE.BuildingsC.Set(buildT, true);
                    _e.SoundAction(ClipTypes.Click).Invoke();
                }
                else
                {
                    _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyBuildingInTownM), buildT });
                }
            }



            switch (buildT)
            {
                case BuildingTypes.House:
                    _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyBuildingInTownM), buildT });
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

            _e.NeedUpdateView = true;
        }
    }
}