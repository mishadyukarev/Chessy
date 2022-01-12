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
                ref var selUnitDatCom = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
                ref var selOwnUnitCom = ref Unit<PlayerC>(SelIdx<IdxC>().Idx);

                ref var selBuildDatCom = ref Build<BuildC>(SelIdx<IdxC>().Idx);
                ref var ownBuildC_sel = ref Build<PlayerC>(SelIdx<IdxC>().Idx);

                var needActiveThirdButt = false;


                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    if (selOwnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        if (selBuildDatCom.Have)
                        {
                            if (ownBuildC_sel.Is(WhoseMoveC.CurPlayerI))
                            {
                                if (!WhereBuildsC.IsSetted(BuildTypes.City, WhoseMoveC.CurPlayerI))
                                {
                                    needActiveThirdButt = true;
                                    UIEntBuild.Button<ImageUIC>(BuildButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                                    BuildAbilC.SetAbility(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                                }
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                UIEntBuild.Button<ImageUIC>(BuildButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.CityNone);
                                BuildAbilC.SetAbility(BuildButtonTypes.Third, BuildAbilTypes.Destroy);
                            }
                        }

                        else
                        {
                            if (!WhereBuildsC.IsSetted(BuildTypes.City, WhoseMoveC.CurPlayerI))
                            {
                                needActiveThirdButt = true;
                                UIEntBuild.Button<ImageUIC>(BuildButtonTypes.Third).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                                BuildAbilC.SetAbility(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                            }
                        }
                    }
                }

                UIEntBuild.Button<ButtonUIC>(BuildButtonTypes.Third).SetActive(needActiveThirdButt);
            }
        }
    }
}
