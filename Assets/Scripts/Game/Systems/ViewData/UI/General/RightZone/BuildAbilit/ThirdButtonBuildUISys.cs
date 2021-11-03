using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class ThirdButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilt = default;

        public void Run()
        {
            if (SelectorC.IsSelCell)
            {
                var idxSelCell = SelectorC.IdxSelCell;

                ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
                ref var selOwnUnitCom = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);

                ref var selBuildDatCom = ref _cellBuildFilt.Get1(SelectorC.IdxSelCell);
                ref var ownBuildC_sel = ref _cellBuildFilt.Get2(SelectorC.IdxSelCell);

                var needActiveThirdButt = false;


                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    if (selOwnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        if (selBuildDatCom.HaveBuild)
                        {
                            if (ownBuildC_sel.Is(WhoseMoveC.CurPlayerI))
                            {
                                if (!WhereBuildsC.IsSettedCity(WhoseMoveC.CurPlayerI))
                                {
                                    needActiveThirdButt = true;
                                    BuildAbilitViewUIC.SetSpriteThird(SpriteGameTypes.City);
                                    BuildAbilitDataUIC.SetAbilityType(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                                }
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                BuildAbilitViewUIC.SetSpriteThird(SpriteGameTypes.CityNone);
                                BuildAbilitDataUIC.SetAbilityType(BuildButtonTypes.Third, BuildAbilTypes.Destroy);
                            }
                        }

                        else
                        {
                            if (!WhereBuildsC.IsSettedCity(WhoseMoveC.CurPlayerI))
                            {
                                needActiveThirdButt = true;
                                BuildAbilitViewUIC.SetSpriteThird(SpriteGameTypes.City);
                                BuildAbilitDataUIC.SetAbilityType(BuildButtonTypes.Third, BuildAbilTypes.CityBuild);
                            }
                        }
                    }
                }

                BuildAbilitViewUIC.SetActive_Button(BuildButtonTypes.Third, needActiveThirdButt);
            }
        }
    }
}
