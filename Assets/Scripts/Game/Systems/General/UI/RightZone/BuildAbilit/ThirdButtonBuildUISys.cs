using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class ThirdButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilt = default;

        public void Run()
        {
            if (SelectorC.IsSelCell)
            {
                var idxSelCell = SelectorC.IdxSelCell;

                ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
                ref var selOwnUnitCom = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);

                ref var selBuildDatCom = ref _cellBuildFilt.Get1(SelectorC.IdxSelCell);
                ref var selOwnBuildCom = ref _cellBuildFilt.Get2(SelectorC.IdxSelCell);

                var needActiveThirdButt = false;


                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    if (selOwnUnitCom.IsMine)
                    {
                        if (selBuildDatCom.HaveBuild)
                        {
                            if (!selOwnBuildCom.IsMine)
                            {
                                needActiveThirdButt = true;
                                BuildAbilitViewUIC.SetSpriteThird(SpriteGameTypes.CityNone);
                                BuildAbilitDataUIC.SetAbilityType(BuildButtonTypes.Third, AbilityTypes.Destroy);
                            }
                        }

                        else
                        {
                            if (BuildsInGameC.IsSettedCity(WhoseMoveC.CurPlayer))
                            {
                                needActiveThirdButt = false;
                            }
                            else
                            {
                                needActiveThirdButt = true;
                                BuildAbilitViewUIC.SetSpriteThird(SpriteGameTypes.City);
                                BuildAbilitDataUIC.SetAbilityType(BuildButtonTypes.Third, AbilityTypes.CityBuild);
                            }
                        }
                    }
                }

                BuildAbilitViewUIC.SetActive_Button(BuildButtonTypes.Third, needActiveThirdButt);
            }
        }
    }
}
