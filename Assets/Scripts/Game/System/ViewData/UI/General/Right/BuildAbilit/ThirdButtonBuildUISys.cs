﻿using Leopotam.Ecs;
using Game.Common;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class ThirdButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _cellUnitFilter = default;

        public void Run()
        {
            if (SelIdx<SelIdxC>().IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelIdx<IdxC>().Idx);
                ref var selOwnUnitCom = ref _cellUnitFilter.Get2(SelIdx<IdxC>().Idx);

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
