﻿using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FirstButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _cellUnitFilt = default;

        public void Run()
        {
            var needActiveButton = false;

            if (IdxSel.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(IdxSel.Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref _cellUnitFilt.Get2(IdxSel.Idx);

                    if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitViewUIC.SetActive_Button(BuildButtonTypes.First, needActiveButton);
        }
    }
}
