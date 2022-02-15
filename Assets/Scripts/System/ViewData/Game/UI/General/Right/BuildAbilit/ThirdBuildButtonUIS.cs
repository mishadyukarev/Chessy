//namespace Game.Game
//{
//    struct ThirdBuildButtonUIS : IEcsRunSystem
//    {
//        public void Run()
//        {
//            //if (Ents.SelectedIdxE.IsSelCell)
//            //{
//            //    var idx_sel = Ents.SelectedIdxE.Idx;

//            //    ref var unit_sel = ref Ents.CellEs.CellUnitEs.Else(Ents.SelectedIdxE.Idx).UnitC;
//            //    ref var ownUnit_sel = ref Ents.CellEs.CellUnitEs.Else(Ents.SelectedIdxE.Idx).OwnerC;

//            //    ref var build_sel = ref CellBuildEs.Build(Ents.SelectedIdxE.Idx).BuildTC;
//            //    ref var ownBuild_sel = ref CellBuildEs.Build(Ents.SelectedIdxE.Idx).PlayerTC;

//            //    var needActiveThirdButt = false;


//            //    if (unit_sel.Is(UnitTypes.Pawn))
//            //    {
//            //        if (ownUnit_sel.Is(Ents.WhoseMove.CurPlayerI))
//            //        {
//            //            if (build_sel.Have)
//            //            {
//            //                if (ownBuild_sel.Is(Ents.WhoseMove.CurPlayerI))
//            //                {
//            //                    if (!WhereBuildsE.IsSetted(BuildingTypes.City, Ents.WhoseMove.CurPlayerI, out var idx_city))
//            //                    {
//            //                        needActiveThirdButt = true;
//            //                        //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.City).Sprite;
//            //                        Ents.CellEs.CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build = BuildingTypes.City;
//            //                    }
//            //                }
//            //                else
//            //                {
//            //                    needActiveThirdButt = true;
//            //                    //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.CityNone).Sprite;
//            //                    Ents.CellEs.CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build = BuildingTypes.None;
//            //                }
//            //            }

//            //            else
//            //            {
//            //                if (!WhereBuildsE.IsSetted(BuildingTypes.City, Ents.WhoseMove.CurPlayerI, out var idx_city))
//            //                {
//            //                    needActiveThirdButt = true;
//            //                    //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.City).Sprite;
//            //                    Ents.CellEs.CellUnitEs.BuildingButton(ButtonTypes.Third, idx_sel).BuildingTC.Build = BuildingTypes.City;
//            //                }
//            //            }
//            //        }
//            //    }

//            //    //RightUIEntities.Building(ButtonTypes.Third).Parent.SetActive(needActiveThirdButt);
//            //    //RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.City).Parent.SetActive(needActiveThirdButt);
//            //    //RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.Mine).Parent.SetActive(false);
//            //    //RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.Farm).Parent.SetActive(false);
//            //}
//        }
//    }
//}
