using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class ThirdButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _cellUnitFilter = default;
        private EcsFilter<BuildC, OwnerC> _cellBuildFilt = default;

        public void Run()
        {
            if (SelIdx.IsSelCell)
            {
                var idxSelCell = SelIdx.Idx;

                ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelIdx.Idx);
                ref var selOwnUnitCom = ref _cellUnitFilter.Get2(SelIdx.Idx);

                ref var selBuildDatCom = ref _cellBuildFilt.Get1(SelIdx.Idx);
                ref var ownBuildC_sel = ref _cellBuildFilt.Get2(SelIdx.Idx);

                var needActiveThirdButt = false;


                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    if (selOwnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        if (selBuildDatCom.Have)
                        {
                            if (ownBuildC_sel.Is(WhoseMoveC.CurPlayerI))
                            {
                                if (!WhereBuildsC.IsSettedCity(WhoseMoveC.CurPlayerI))
                                {
                                    needActiveThirdButt = true;
                                    BuildAbilitUIC.SetSpriteThird(SpriteGameTypes.City);
                                    BuildAbilC.SetAbilityType(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                                }
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                BuildAbilitUIC.SetSpriteThird(SpriteGameTypes.CityNone);
                                BuildAbilC.SetAbilityType(BuildButtonTypes.Third, BuildAbilTypes.Destroy);
                            }
                        }

                        else
                        {
                            if (!WhereBuildsC.IsSettedCity(WhoseMoveC.CurPlayerI))
                            {
                                needActiveThirdButt = true;
                                BuildAbilitUIC.SetSpriteThird(SpriteGameTypes.City);
                                BuildAbilC.SetAbilityType(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                            }
                        }
                    }
                }

                BuildAbilitUIC.SetActive_Button(BuildButtonTypes.Third, needActiveThirdButt);
            }
        }
    }
}
