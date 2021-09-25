using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.View.UI.Game.General;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.RightZone
{
    internal sealed class FirstButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var selCom = ref _selFilt.Get1(0);
            ref var buildAbilUICom = ref _buildAbilUIFilt.Get1(0);


            var needActiveButton = false;

            if (selCom.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(selCom.IdxSelCell);

                buildAbilUICom.SetText_Button(BuildButtonTypes.First, LanguageComCom.GetText(GameLanguageTypes.BuildFarm));


                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref _cellUnitFilt.Get2(selCom.IdxSelCell);

                    if (selOnUnitCom.IsMine)
                    {
                        needActiveButton = true;
                    }
                }
            }

            buildAbilUICom.SetActive_Button(BuildButtonTypes.First, needActiveButton);
        }
    }
}
