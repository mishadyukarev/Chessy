//using Chessy.Model.Values.Environment;
//using Chessy.Model.Values;
//using Chessy.Model.Values;
//using Chessy.Model.Values;
//using Photon.Realtime;

//using Chessy.Model.Entity; namespace Chessy.Model.Master.Methods
//{
//    public struct DirectWaveSnowyMS
//    {
//        public DirectWaveSnowyMS(in byte idx_from, in byte idx_to, in Player sender, in Chessy.Model.EntitiesModel e)
//        {
//            var whoseMove = e.WhoseMove.Player;

//            var direct = e.CellEs(idx_from).Direct(idx_to);

//            if (direct == DirectTypes.None) return;


//            if (e.UnitWaterC(idx_from).Water >= WaterValues.DIRECT_WAVE || e.RiverEs(idx_from).RiverTC.HaveRiverNear)
//            {
//                if (e.UnitStepC(idx_from).Steps >= StepValues.DIRECT_WAVE)
//                {
//                    if (!e.RiverEs(idx_from).RiverTC.HaveRiverNear) e.UnitWaterC(idx_from).Water -= WaterValues.DIRECT_WAVE;
//                    e.UnitStepC(idx_from).Steps -= StepValues.DIRECT_WAVE;
//                    e.UnitEs(idx_from).CoolDownC(AbilityTypes.DirectWave).Cooldown = AbilityCooldownValues.DIRECT_WAVE;

//                    e.FertilizeC(idx_from).Resources = ValuesChessy.MAX_RESOURCES;

//                    var cell_0 = idx_to;

//                    for (var i = 0; i < 3; i++)
//                    {
//                        if (!e.IsActiveParentSelf(cell_0)) break;


//                        e.FertilizeC(cell_0).Resources = ValuesChessy.MAX_RESOURCES;

//                        if (e.UnitTC(cell_0).HaveUnit())
//                        {
//                            if (e.UnitPlayerTC(cell_0).Is(whoseMove))
//                            {
//                                e.UnitWaterC(cell_0).Water = WaterValues.MAX;
//                            }
//                        }

//                        e.HaveFire(cell_0) = false;

//                        cell_0 = e.CellEs(cell_0).AroundCellE(direct).IdxC.Idx;
//                    }
//                }

//                else
//                {
//                    e.RpcPoolEs.ExecuteMethodWithRpcS.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
//                }
//            }

//            else
//            {
//                e.RpcPoolEs.ExecuteMethodWithRpcS.SoundToGeneral(sender, ClipTypes.Mistake);
//            }
//        }
//    }
//}