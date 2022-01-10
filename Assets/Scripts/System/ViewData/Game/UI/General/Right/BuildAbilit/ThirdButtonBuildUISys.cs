using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ThirdButtonBuildUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (SelIdx<SelIdxC>().IsSelCell)
            {
                ref var selUnitDatCom = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
                ref var selOwnUnitCom = ref Unit<OwnerC>(SelIdx<IdxC>().Idx);

                ref var selBuildDatCom = ref Build<BuildC>(SelIdx<IdxC>().Idx);
                ref var ownBuildC_sel = ref Build<OwnerC>(SelIdx<IdxC>().Idx);

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
                                    BuildAbilitUIC.SetSpriteThird(SpriteTypes.City);
                                    BuildAbilC.SetAbility(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                                }
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                BuildAbilitUIC.SetSpriteThird(SpriteTypes.CityNone);
                                BuildAbilC.SetAbility(BuildButtonTypes.Third, BuildAbilTypes.Destroy);
                            }
                        }

                        else
                        {
                            if (!WhereBuildsC.IsSetted(BuildTypes.City, WhoseMoveC.CurPlayerI))
                            {
                                needActiveThirdButt = true;
                                BuildAbilitUIC.SetSpriteThird(SpriteTypes.City);
                                BuildAbilC.SetAbility(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                            }
                        }
                    }
                }

                BuildAbilitUIC.SetActive_Button(BuildButtonTypes.Third, needActiveThirdButt);
            }
        }
    }
}
