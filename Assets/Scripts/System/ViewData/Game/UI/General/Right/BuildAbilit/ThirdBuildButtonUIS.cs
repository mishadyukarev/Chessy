using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;
using static Game.Game.CellBuildE;

namespace Game.Game
{
    struct ThirdBuildButtonUIS : IEcsRunSystem
    {
        public void Run()
        {
            if (EntitiesPool.SelectedIdxE.IsSelCell)
            {
                var idx_sel = EntitiesPool.SelectedIdxE.IdxC.Idx;

                ref var unit_sel = ref Unit(EntitiesPool.SelectedIdxE.IdxC.Idx);
                ref var ownUnit_sel = ref EntitiesPool.UnitElse.Owner(EntitiesPool.SelectedIdxE.IdxC.Idx);

                ref var build_sel = ref Build<BuildingTC>(EntitiesPool.SelectedIdxE.IdxC.Idx);
                ref var ownBuild_sel = ref Build<PlayerTC>(EntitiesPool.SelectedIdxE.IdxC.Idx);

                var needActiveThirdButt = false;


                if (unit_sel.Is(UnitTypes.Pawn))
                {
                    if (ownUnit_sel.Is(WhoseMoveE.CurPlayerI))
                    {
                        if (build_sel.Have)
                        {
                            if (ownBuild_sel.Is(WhoseMoveE.CurPlayerI))
                            {
                                if (!WhereBuildsE.IsSetted(BuildingTypes.City, WhoseMoveE.CurPlayerI, out var idx_city))
                                {
                                    needActiveThirdButt = true;
                                    //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.City).Sprite;
                                    CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build = BuildingTypes.City;
                                }
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.CityNone).Sprite;
                                CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build = BuildingTypes.None;
                            }
                        }

                        else
                        {
                            if (!WhereBuildsE.IsSetted(BuildingTypes.City, WhoseMoveE.CurPlayerI, out var idx_city))
                            {
                                needActiveThirdButt = true;
                                //RightBuildUIE.Button<ImageUIC>(ButtonTypes.Third).Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.City).Sprite;
                                CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build = BuildingTypes.City;
                            }
                        }
                    }
                }

                RightUIEntities.Building(ButtonTypes.Third).Parent.SetActive(needActiveThirdButt);
                RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.City).Parent.SetActive(needActiveThirdButt);
                RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.Mine).Parent.SetActive(false);
                RightUIEntities.BuildingZone(ButtonTypes.Third, BuildingTypes.Farm).Parent.SetActive(false);
            }
        }
    }
}
