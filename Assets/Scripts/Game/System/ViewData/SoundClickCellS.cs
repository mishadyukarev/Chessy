using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SoundClickCellS : IEcsRunSystem
    {
        //private EcsFilter<UnitC, LevelC, OwnerC, VisibleC> _unitF = default;

        public void Run()
        {

            if (NeedSoundEffC.Clip != default)
            {
                SoundEffectVC.Play(NeedSoundEffC.Clip);
            }


            //ref var unit_sel = ref _unitF.Get1(SelIdx.Idx);
            //ref var own_sel = ref _unitF.Get3(SelIdx.Idx);


            //if (InputC.IsClicked)
            //{
            //    if (RayCastC.Is(RaycastTypes.Cell))
            //    {
            //        if (CellClickC.Is(CellClickTypes.None, CellClickTypes.SelCell))
            //        {
            //            if (unit_sel.HaveUnit)
            //            {
            //                if (own_sel.Is(WhoseMoveC.CurPlayerI))
            //                {
            //                    if (!CellsShiftC.HaveIdxCell(WhoseMoveC.CurPlayerI, PreIdx.Idx, SelIdx.Idx))
            //                    {
            //                        if (unit_sel.Is(UnitTypes.Scout))
            //                        {

            //                        }
            //                        else if (unit_sel.IsMelee)
            //                        {
            //                            SoundEffectC.Play(ClipTypes.PickMelee);
            //                        }
            //                        else
            //                        {
            //                            SoundEffectC.Play(ClipTypes.PickArcher);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}