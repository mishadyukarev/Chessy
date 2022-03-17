//using Chessy.Game.Values.Cell.Environment;
//using Chessy.Game.Values.Cell.Unit;
//using Chessy.Game.Values.Cell.Unit.Effect;
//using Chessy.Game.Values.Cell.Unit.Stats;
//using Photon.Realtime;

//namespace Chessy.Game.System.Model.Master.Methods
//{
//    public struct DirectWaveSnowyMS
//    {
//        public DirectWaveSnowyMS(in byte idx_from, in byte idx_to, in Player sender, in EntitiesModel e)
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

//                    e.FertilizeC(idx_from).Resources = EnvironmentValues.MAX_RESOURCES;

//                    var idx_0 = idx_to;

//                    for (var i = 0; i < 3; i++)
//                    {
//                        if (!e.CellEs(idx_0).IsActiveParentSelf) break;


//                        e.FertilizeC(idx_0).Resources = EnvironmentValues.MAX_RESOURCES;

//                        if (e.UnitTC(idx_0).HaveUnit)
//                        {
//                            if (e.UnitPlayerTC(idx_0).Is(whoseMove))
//                            {
//                                e.UnitWaterC(idx_0).Water = WaterValues.MAX;
//                            }
//                        }

//                        e.HaveFire(idx_0) = false;

//                        idx_0 = e.CellEs(idx_0).AroundCellE(direct).IdxC.Idx;
//                    }
//                }

//                else
//                {
//                    e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
//                }
//            }

//            else
//            {
//                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
//            }
//        }
//    }
//}