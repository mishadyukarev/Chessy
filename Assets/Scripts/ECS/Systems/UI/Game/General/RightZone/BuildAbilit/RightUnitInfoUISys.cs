using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.RightZone.BuildAbilit
{
    internal sealed class RightUnitInfoUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilUIFilt = default;
        private EcsFilter<CondUnitUICom> _condUnitUIFilt = default;
        private EcsFilter<UniqueAbiltUICom> _uniqueAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var selCom = ref _selFilt.Get1(0);
            ref var condUnitUICom = ref _condUnitUIFilt.Get1(0);
            ref var uniqueAbilUICom = ref _uniqueAbilUIFilt.Get1(0);
            ref var buildAbilUICom = ref _buildAbilUIFilt.Get1(0);


            var needActiveInfoText = false;

            if (selCom.IsSelectedCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(selCom.IdxSelCell);


                if (selUnitDatCom.HaveUnit)
                {
                    ref var selOffUnitCom = ref _cellUnitFilt.Get3(selCom.IdxSelCell);

                    if (selOffUnitCom.HaveLocalPlayer)
                    {
                        if (selOffUnitCom.IsMine)
                        {
                            condUnitUICom.SetText_Info(LanguageComComp.GetText(GameLanguageTypes.ConditAbilities));
                            uniqueAbilUICom.SetTextInfo(LanguageComComp.GetText(GameLanguageTypes.UniqueAbilities));
                            buildAbilUICom.SetTextInfo(LanguageComComp.GetText(GameLanguageTypes.BuildingAbilities));
                            needActiveInfoText = true;
                        }
                    }
                }
            }

            condUnitUICom.SetActiveInfo(needActiveInfoText);
            uniqueAbilUICom.SetActiveInfo(needActiveInfoText);
            buildAbilUICom.ActiveInfo(needActiveInfoText);
        }
    }
}
