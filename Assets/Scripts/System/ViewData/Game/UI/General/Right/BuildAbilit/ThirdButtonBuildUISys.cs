using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityPool;
using static Game.Game.EntityCellBuildPool;

namespace Game.Game
{
    struct ThirdButtonBuildUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (SelIdx<SelIdxEC>().IsSelCell)
            {
                var idx_sel = SelIdx<IdxC>().Idx;

                ref var selUnitDatCom = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
                ref var selOwnUnitCom = ref Unit<PlayerC>(SelIdx<IdxC>().Idx);

                ref var selBuildDatCom = ref Build<BuildingC>(SelIdx<IdxC>().Idx);
                ref var ownBuildC_sel = ref Build<PlayerC>(SelIdx<IdxC>().Idx);

                var needActiveThirdButt = false;


                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    if (selOwnUnitCom.Is(EntWhoseMove.CurPlayerI))
                    {
                        if (selBuildDatCom.Have)
                        {
                            if (ownBuildC_sel.Is(EntWhoseMove.CurPlayerI))
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
                                UnitBuilding<BuildingC>(ButtonTypes.Third, idx_sel).Build = BuildTypes.None;
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
