//using UnityEngine;

//namespace Game.Game
//{
//    sealed class CellUnitFliperAndRotatorVS : SystemViewAbstract, IEcsRunSystem
//    {
//        internal CellUnitFliperAndRotatorVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
//        {
//        }

//        public void Run()
//        {
//            //for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
//            //{
//            //    var unit_0 = UnitEs(idx_0).TypeE.UnitTC;
//            //    var ownUnit_0 = UnitE(idx_0).OwnerC;
//            //    var isCorned = UnitEs(idx_0).CornedE.IsCornered;

//            //    ref var main_0 = ref CellVEs(idx_0).UnitVEs.UnitMainSR;
//            //    ref var extra_0 = ref CellVEs(idx_0).UnitVEs.UnitExtraSR;




//            //    main_0.LocalEulerAngles = new Vector3(0, 0, 0);
//            //    main_0.FlipX = false;
//            //    extra_0.FlipX = false;

//            //    if (Es.SelectedIdxE.IdxC.Is(idx_0))
//            //    {
//            //        if (UnitEs(idx_0).TypeE.HaveUnit)
//            //        {
//            //            if (ownUnit_0.Is(Es.WhoseMoveE.CurPlayerI))
//            //            {
//            //                if (unit_0.Is(UnitTypes.Archer))
//            //                {
//            //                    if (isCorned)
//            //                    {
//            //                        main_0.LocalEulerAngles = new Vector3(0, 0, -90);
//            //                        main_0.FlipX = false;
//            //                    }
//            //                    else
//            //                    {
//            //                        main_0.LocalEulerAngles = new Vector3(0, 0, 0);
//            //                        main_0.FlipX = true;
//            //                    }
//            //                }
//            //                else
//            //                {
//            //                    main_0.FlipX = true;
//            //                    extra_0.FlipX = true;
//            //                }
//            //            }
//            //        }
//            //    }
//            //}
//        }
//    }
//}
