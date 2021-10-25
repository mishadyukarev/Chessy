using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class ThirdButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilViewCom = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilt = default;

        public void Run()
        {
            ref var selCom = ref _selFilt.Get1(0);

            if (selCom.IsSelCell)
            {
                var idxSelCell = _selFilt.Get1(0).IdxSelCell;

                ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
                ref var selOwnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);

                ref var selBuildDatCom = ref _cellBuildFilt.Get1(idxSelCell);
                ref var selOwnBuildCom = ref _cellBuildFilt.Get2(idxSelCell);

                ref var buildAbilUICom = ref _buildAbilViewCom.Get1(0);

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
                                buildAbilUICom.SetSpriteThird(SpriteGameTypes.CityNone);
                                //buildAbilUICom.SetSpriteThird(SpriteGameTypes.City);
                                //buildAbilUICom.SetText_Button(BuildButtonTypes.Third, LanguageComCom.GetText(GameLanguageTypes.DestroyBuilding));
                            }
                        }

                        else
                        {
                            var isSettedMyCity = false;

                            buildAbilUICom.SetSpriteThird(SpriteGameTypes.City);
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

                buildAbilUICom.SetActive_Button(BuildButtonTypes.Third, needActiveThirdButt);
            }
        }
    }
}
