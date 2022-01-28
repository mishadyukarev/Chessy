namespace Game.Game
{
    struct ThirdBuildButtonUIS : IEcsRunSystem
    {
        public void Run()
        {
            //if (Entities.SelectedIdxE.IsSelCell)
            //{
            //    var idx_sel = Entities.SelectedIdxE.IdxC.Idx;

            //    ref var unit_sel = ref CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).UnitC;
            //    ref var ownUnit_sel = ref CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).OwnerC;

            //    ref var build_sel = ref CellBuildEs.Build(Entities.SelectedIdxE.IdxC.Idx).BuildTC;
            //    ref var ownBuild_sel = ref CellBuildEs.Build(Entities.SelectedIdxE.IdxC.Idx).PlayerTC;

            //    var needActiveThirdButt = false;


            //    if (unit_sel.Is(UnitTypes.Pawn))
            //    {
            //        if (ownUnit_sel.Is(Entities.WhoseMove.CurPlayerI))
            //        {
            //            if (build_sel.Have)
            //            {
            //                if (ownBuild_sel.Is(Entities.WhoseMove.CurPlayerI))
            //                {
            //                    if (!WhereBuildsE.IsSetted(BuildingTypes.City, Entities.WhoseMove.CurPlayerI, out var idx_city))
            //                    {
            //                        needActiveThirdButt = true;
            //                        //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.City).Sprite;
            //                        CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build = BuildingTypes.City;
            //                    }
            //                }
            //                else
            //                {
            //                    needActiveThirdButt = true;
            //                    //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.CityNone).Sprite;
            //                    CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build = BuildingTypes.None;
            //                }
            //            }

            //            else
            //            {
            //                if (!WhereBuildsE.IsSetted(BuildingTypes.City, Entities.WhoseMove.CurPlayerI, out var idx_city))
            //                {
            //                    needActiveThirdButt = true;
            //                    //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.City).Sprite;
            //                    CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build = BuildingTypes.City;
            //                }
            //            }
            //        }
            //    }

            //    //RightUIEntities.Building(ButtonTypes.Third).Parent.SetActive(needActiveThirdButt);
            //    //RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.City).Parent.SetActive(needActiveThirdButt);
            //    //RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.Mine).Parent.SetActive(false);
            //    //RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.Farm).Parent.SetActive(false);
            //}
        }
    }
}
