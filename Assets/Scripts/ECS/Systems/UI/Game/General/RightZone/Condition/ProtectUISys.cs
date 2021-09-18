using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Components.View.UI.Game.General.Right
{
    internal class ProtectUISys : IEcsRunSystem
    {
        private EcsFilter<CondUnitUICom> _condUnitUIFilt = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;

        public void Run()
        {
            var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;
            ref var condUnitUICom = ref _condUnitUIFilt.Get1(0);

            ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
            ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
            ref var selOffUnitCom = ref _cellUnitFilter.Get3(idxSelCell);
            ref var selBotUnitCom = ref _cellUnitFilter.Get4(idxSelCell);


            var isEnableButt = false;


            if (selUnitDatCom.HaveUnit)
            {
                condUnitUICom.SetText_Button(CondUnitTypes.Protected, LanguageComComp.GetText(GameLanguageTypes.Protect));


                if (selOnUnitCom.HaveOwner)
                {

                }

                else if (selOffUnitCom.HaveLocalPlayer)
                {
                    if (selOffUnitCom.IsMine)
                    {
                        isEnableButt = true;

                    }
                }

                else if (selBotUnitCom.IsBot)
                {


                }
            }


            condUnitUICom.SetActive(CondUnitTypes.Protected, isEnableButt);
        }
    }
}
