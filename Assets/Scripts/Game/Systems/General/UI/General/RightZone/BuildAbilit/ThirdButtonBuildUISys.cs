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
                                BuildAbilitUIC.SetSpriteThird(SpriteGameTypes.CityNone);
                                //buildAbilUICom.SetSpriteThird(SpriteGameTypes.City);
                                //buildAbilUICom.SetText_Button(BuildButtonTypes.Third, LanguageComCom.GetText(GameLanguageTypes.DestroyBuilding));
                            }
                        }

                        else
                        {
                            var isSettedMyCity = false;

                            BuildAbilitUIC.SetSpriteThird(SpriteGameTypes.City);
                            //buildAbilUICom.SetText_Button(BuildButtonTypes.Third, LanguageComCom.GetText(GameLanguageTypes.BuildCity));

                            foreach (var idxCell in _cellBuildFilt)
                            {
                                ref var curBuildDatCom = ref _cellBuildFilt.Get1(idxCell);
                                ref var curOwnBuildCom = ref _cellBuildFilt.Get2(idxCell);

                                if (curBuildDatCom.Is(BuildingTypes.City))
                                {
                                    if (curOwnBuildCom.IsMine)
                                    {

                                        isSettedMyCity = true;
                                        break;
                                    }
                                }
                            }

                            if (isSettedMyCity)
                            {
                                needActiveThirdButt = false;
                            }
                            else
                            {
                                needActiveThirdButt = true;
                            }
                        }
                    }
                }

                BuildAbilitUIC.SetActive_Button(BuildButtonTypes.Third, needActiveThirdButt);
            }
        }
    }
}
