﻿using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Sync.Unit
{
    internal sealed class SyncCellSelUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, CellUnitMainViewComp> _cellUnitFilter = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;

        public void Run()
        {
            ref var selCom = ref _selectorFilter.Get1(0);



            if (selCom.IsSelUnit)
            {
                var idxCurCell = selCom.IdxCurCell;
                var idxPreCell = selCom.IdxPreVisionCell;


                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curMainUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);

                ref var preVisMainUnitViewCom = ref _cellUnitFilter.Get2(idxPreCell);


                if (curUnitDatCom.HaveUnit)
                {
                    if (curUnitDatCom.IsVisibleUnit(WhoseMoveCom.CurPlayer))
                    {
                        preVisMainUnitViewCom.Enable_SR();

                        if (selCom.SelUnitType == UnitTypes.King)
                        {
                            preVisMainUnitViewCom.SetKing_Sprite();
                        }
                        else if (selCom.SelUnitType == UnitTypes.Pawn)
                        {
                            preVisMainUnitViewCom.SetPawn_Spriter();
                        }
                        else if (selCom.SelUnitType == UnitTypes.Rook || selCom.SelUnitType == UnitTypes.Bishop)
                        {
                            preVisMainUnitViewCom.SetArcher_Sprite(selCom.SelUnitType, ToolWeaponTypes.Bow);
                        }
                    }

                    else
                    {
                        curMainUnitViewCom.Enable_SR();

                        if (selCom.SelUnitType == UnitTypes.King)
                        {
                            curMainUnitViewCom.SetKing_Sprite();
                        }
                        else if (selCom.SelUnitType == UnitTypes.Pawn)
                        {
                            curMainUnitViewCom.SetPawn_Spriter();
                        }
                        else if (selCom.SelUnitType == UnitTypes.Rook || selCom.SelUnitType == UnitTypes.Bishop)
                        {
                            curMainUnitViewCom.SetArcher_Sprite(selCom.SelUnitType, ToolWeaponTypes.Bow);
                        }


                    }
                }

                else
                {
                    curMainUnitViewCom.Enable_SR();

                    if (selCom.SelUnitType == UnitTypes.King)
                    {
                        curMainUnitViewCom.SetKing_Sprite();
                    }
                    else if (selCom.SelUnitType == UnitTypes.Pawn)
                    {
                        curMainUnitViewCom.SetPawn_Spriter();
                    }
                    else if (selCom.SelUnitType == UnitTypes.Rook || selCom.SelUnitType == UnitTypes.Bishop)
                    {
                        curMainUnitViewCom.SetArcher_Sprite(selCom.SelUnitType, ToolWeaponTypes.Bow);
                    }
                }
            }
        }
    }
}