using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _currentGameWorld;

        private EcsFilter<InfoMasCom> _infoFilter;
        private EcsFilter<FireMasCom, XyFromToComponent> _fireFilter;

        public void Init()
        {
            _currentGameWorld.NewEntity()
                .Replace(new FireMasCom())
                .Replace(new XyFromToComponent(new int[2], new int[2]));
        }

        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var fromXy = _fireFilter.Get2(0).FromXy;
            var toXy = _fireFilter.Get2(0).ToXy;


            if (CellUnitsDataSystem.IsMelee(fromXy))
            {
                if (CellUnitsDataSystem.HaveMinAmountSteps(fromXy))
                {
                    if (CellFireDataSystem.HaveFireCom(toXy).HaveFire)
                    {
                        CellFireDataSystem.HaveFireCom(toXy).Disable();

                        CellUnitsDataSystem.TakeAmountSteps(toXy);
                    }
                    else if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, toXy))
                    {
                        if (CellUnitsDataSystem.HaveOwner(fromXy))
                        {
                            RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            CellFireDataSystem.HaveFireCom(toXy).Enable();
                            CellUnitsDataSystem.TakeAmountSteps(toXy);
                        }

                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                else
                {
                    RPCGameSystem.MistakeStepsUnitToGeneral(sender);
                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
            }

            else
            {
                if (CellUnitsDataSystem.HaveMaxAmountSteps(fromXy))
                {
                    if (!CellFireDataSystem.HaveFireCom(toXy).HaveFire)
                    {
                        foreach (var xy1 in CellSpaceSupport.TryGetXyAround(fromXy))
                        {
                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                            {
                                if (xy1.Compare(toXy))
                                {
                                    CellUnitsDataSystem.ResetAmountSteps(fromXy);
                                    CellFireDataSystem.HaveFireCom(toXy).Enable();
                                }
                            }
                        }
                    }
                }

                else
                {
                    RPCGameSystem.MistakeStepsUnitToGeneral(sender);
                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
            }
        }
    }
}
