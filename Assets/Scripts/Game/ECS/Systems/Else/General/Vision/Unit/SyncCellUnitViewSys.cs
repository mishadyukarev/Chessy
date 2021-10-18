using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitFilter = default;

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
                        curMainUnitViewCom.SetSprite(curUnitDatCom.UnitType, curUnitDatCom.UpgradeUnitType);


                        if (curUnitDatCom.Is(UnitTypes.Pawn))
                        {
                            if (curUnitDatCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.TWExtraPawnType);
                            }
                        }

                        else if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {

                            //curMainUnitViewCom.SetArcher_Sprite(curUnitDatCom.UnitType, curUnitDatCom.ArcherWeapType);

                            //if (curUnitDatCom.HaveArcherWeapon)
                            //{
                            //    curMainUnitViewCom.SetArcher_Sprite(curUnitDatCom.UnitType, curUnitDatCom.ArcherWeapType);
                            //}
                            //else
                            //{
                            //    throw new Exception();
                            //}

                            //if (curUnitDatCom.HaveExtraToolWeaponPawn)
                            //{
                            //    curExtraUnitViewCom.Enable_SR();
                            //    curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.TWExtraPawnType);
                            //}
                        }

                        //else
                        //{
                        //    throw new Exception();
                        //}


                        curMainUnitViewCom.SetAlpha(curUnitDatCom.IsVisibleUnit(WhoseMoveCom.NextPLayer));
                        curExtraUnitViewCom.SetAlpha(curUnitDatCom.IsVisibleUnit(WhoseMoveCom.NextPLayer));
                    }
                }
            }
        }
    }
}
