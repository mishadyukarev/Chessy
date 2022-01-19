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
            if (SelIdx<SelIdxEC>().IsSelCell)
            {
                var idx_sel = SelIdx<IdxC>().Idx;

                ref var unit_sel = ref Unit<UnitTC>(SelIdx<IdxC>().Idx);
                ref var ownUnit_sel = ref Unit<PlayerTC>(SelIdx<IdxC>().Idx);

                ref var build_sel = ref Build<BuildingTC>(SelIdx<IdxC>().Idx);
                ref var ownBuild_sel = ref Build<PlayerTC>(SelIdx<IdxC>().Idx);

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
                                    UIEntBuild.Button<ImageUIC>(ButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                                    CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build = BuildingTypes.City;
                                }
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                UIEntBuild.Button<ImageUIC>(ButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.CityNone);
                                CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build = BuildingTypes.None;
                            }
                        }

                        else
                        {
                            if (!WhereBuildsE.IsSetted(BuildingTypes.City, WhoseMoveE.CurPlayerI, out var idx_city))
                            {
                                needActiveThirdButt = true;
                                UIEntBuild.Button<ImageUIC>(ButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                                CellUnitBuildingButtonEs.UnitBuildButton<BuildingTC>(ButtonTypes.Third, idx_sel).Build = BuildingTypes.City;
                            }
                        }
                    }
                }

                UIEntBuild.Button<ButtonUIC>(ButtonTypes.Third).SetActive(needActiveThirdButt);
            }
        }
    }
}
