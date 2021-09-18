﻿using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.RightZone
{
    internal sealed class FirstButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var selCom = ref _selFilt.Get1(0);
            ref var buildAbilUICom = ref _buildAbilUIFilt.Get1(0);


            var needActiveButton = false;

            if (selCom.IsSelectedCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(selCom.IdxSelCell);


                if (selUnitDatCom.IsUnit(UnitTypes.Pawn))
                {
                    ref var selOffUnitCom = ref _cellUnitFilt.Get3(selCom.IdxSelCell);

                    if (selOffUnitCom.HaveLocalPlayer)
                    {
                        if (selOffUnitCom.IsMine)
                        {
                            buildAbilUICom.SetText_Button(BuildButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.BuildFarm));
                            needActiveButton = true;
                        }
                    }
                }
            }

            buildAbilUICom.SetActive_Button(BuildButtonTypes.First, needActiveButton);
        }
    }
}