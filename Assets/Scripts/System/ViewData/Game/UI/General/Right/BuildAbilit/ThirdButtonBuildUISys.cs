using static Game.Game.CellE;
using static Game.Game.CellUnitE;
using static Game.Game.EntityPool;
using static Game.Game.CellBuildE;

namespace Game.Game
{
    struct ThirdButtonBuildUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (SelIdx<SelIdxEC>().IsSelCell)
            {
                var idx_sel = SelIdx<IdxC>().Idx;

                ref var selUnitDatCom = ref Unit<UnitTC>(SelIdx<IdxC>().Idx);
                ref var selOwnUnitCom = ref Unit<PlayerTC>(SelIdx<IdxC>().Idx);

                ref var selBuildDatCom = ref Build<BuildingC>(SelIdx<IdxC>().Idx);
                ref var ownBuildC_sel = ref Build<PlayerTC>(SelIdx<IdxC>().Idx);

                var needActiveThirdButt = false;


                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    if (selOwnUnitCom.Is(WhoseMoveE.CurPlayerI))
                    {
                        if (selBuildDatCom.Have)
                        {
                            if (ownBuildC_sel.Is(WhoseMoveE.CurPlayerI))
                            {
                                //if (!EntWhereBuilds.IsSetted(BuildTypes.City, WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI))
                                //{
                                //    needActiveThirdButt = true;
                                //    UIEntBuild.Button<ImageUIC>(ButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                                //    UnitBuilding<BuildingC>(ButtonTypes.Third, idx_sel).Build = BuildTypes.City;
                                //}
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                UIEntBuild.Button<ImageUIC>(ButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.CityNone);
                                UnitBuildButton<BuildingC>(ButtonTypes.Third, idx_sel).Build = BuildTypes.None;
                            }
                        }

                        else
                        {
                            //if (!EntWhereBuilds.IsSetted(BuildTypes.City, WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI))
                            //{
                            //    needActiveThirdButt = true;
                            //    UIEntBuild.Button<ImageUIC>(ButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                            //    UnitBuilding<BuildingC>(ButtonTypes.Third, idx_sel).Build = BuildTypes.City;
                            //}
                        }
                    }
                }

                UIEntBuild.Button<ButtonUIC>(ButtonTypes.Third).SetActive(needActiveThirdButt);
            }
        }
    }
}
