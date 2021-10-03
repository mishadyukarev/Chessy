using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, CellUnitMainViewComp, CellUnitExtraViewComp> _cellUnitFilter = default;

        public void Run()
        {
            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curMainUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);
                ref var curExtraUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);

                curMainUnitViewCom.Disable_SR();
                curExtraUnitViewCom.Disable_SR();


                if (curUnitDatCom.HaveUnit)
                {
                    if (curUnitDatCom.IsVisibleUnit(WhoseMoveCom.CurPlayer))
                    {
                        curMainUnitViewCom.Enable_SR();

                        if (curUnitDatCom.Is(UnitTypes.King))
                        {
                            curMainUnitViewCom.SetKing_Sprite();
                        }

                        else if (curUnitDatCom.Is(UnitTypes.Pawn))
                        {
                            curMainUnitViewCom.SetPawn_Spriter();


                            if (curUnitDatCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.ExtraTWPawnType);
                            }
                        }

                        else if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                            if (curUnitDatCom.HaveArcherWeapon)
                            {
                                curMainUnitViewCom.SetArcher_Sprite(curUnitDatCom.UnitType, curUnitDatCom.ArcherWeapType);
                            }
                            else
                            {
                                throw new Exception();
                            }

                            if (curUnitDatCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.ExtraTWPawnType);
                            }
                        }

                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
        }
    }
}
