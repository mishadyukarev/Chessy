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
                if (_selectedBuildingsInTownC.Is(buildT))
                {
                    _selectedBuildingsInTownC.Set(buildT, false);
                    _e.SoundAction(ClipTypes.Click).Invoke();
                }
                else if (_e.PlayerInfoE(_aboutGameC.CurrentPlayerIT).BuildingsInTownInfoC.HaveBuilding(buildT))
                {
                    _selectedBuildingsInTownC.Set(buildT, true);
                    _e.SoundAction(ClipTypes.Click).Invoke();
                }
                else
                {
                    _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyBuildingInTownM), buildT });
                }
            }



            switch (buildT)
            {
                case BuildingTypes.House:
                    _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyBuildingInTownM), buildT });
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

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}