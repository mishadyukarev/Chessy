using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.RightZone
{
    internal sealed class ThirdButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilViewCom = default;

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellBuildFilt = default;

        public void Run()
        {
            ref var selCom = ref _selFilt.Get1(0);

            var idxSelCell = _selFilt.Get1(0).IdxSelCell;

            ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
            ref var selOffUnitCom = ref _cellUnitFilter.Get3(idxSelCell);
            ref var selBotUnitCom = ref _cellUnitFilter.Get4(idxSelCell);

            ref var selBuildDatCom = ref _cellBuildFilt.Get1(idxSelCell);
            ref var selOnBuildCom = ref _cellBuildFilt.Get2(idxSelCell);
            ref var selOffBuildCom = ref _cellBuildFilt.Get3(idxSelCell);
            ref var selBotBuildCom = ref _cellBuildFilt.Get4(idxSelCell);

            ref var buildAbilUICom = ref _buildAbilViewCom.Get1(0);



            var needActiveThirdButt = false;

            if (selCom.IsSelectedCell)
            {
                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    if (selOffUnitCom.HaveLocPlayer)
                    {
                        if (selOffUnitCom.IsMine)
                        {
                            foreach (var idxCell in _cellBuildFilt)
                            {
                                ref var curBuildDatCom = ref _cellBuildFilt.Get1(idxCell);
                                ref var curOffBuildCom = ref _cellBuildFilt.Get3(idxCell);

                                if (curBuildDatCom.HaveBuild)
                                {
                                    if (curOffBuildCom.HaveLocPlayer)
                                    {
                                        if (curOffBuildCom.IsMastMain == WhoseMoveCom.IsMainMove)
                                        {
                                            buildAbilUICom.SetText_Button(BuildButtonTypes.Third, LanguageComComp.GetText(GameLanguageTypes.BuildCity));
                                            needActiveThirdButt = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        needActiveThirdButt = true;
                                    }
                                }
                                else
                                {
                                    needActiveThirdButt = true;
                                }
                            }
                        }
                    }
                }
            }


            buildAbilUICom.SetActive_Button(BuildButtonTypes.Third, needActiveThirdButt);
        }
    }
}
