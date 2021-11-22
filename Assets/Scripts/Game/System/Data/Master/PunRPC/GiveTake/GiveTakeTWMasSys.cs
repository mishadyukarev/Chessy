using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GiveTakeTWMasSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            TWDoingMC.Get(out var tWTypeForGive, out var levelTWType);


            if (idx_0 != default)
            {
                var sender = InfoC.Sender(MGOTypes.Master);

                ref var unit_0 = ref _unitF.Get1(idx_0);

                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

                ref var twUnit_0 = ref _twUnitF.Get1(idx_0);


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (stepUnit_0.HaveMinSteps)
                    {

                        if (twUnit_0.HaveToolWeap)
                        {
                            InvTWC.Add(ownUnit_0.Owner, twUnit_0.ToolWeapon, twUnit_0.LevelTWType);
                            twUnit_0.ToolWeapon = default;

                            stepUnit_0.TakeSteps();

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (InvTWC.Have(ownUnit_0.Owner, tWTypeForGive, levelTWType))
                        {
                            InvTWC.Take(ownUnit_0.Owner, tWTypeForGive, levelTWType);

                            twUnit_0.ToolWeapon = tWTypeForGive;
                            twUnit_0.LevelTWType = levelTWType;
                            if (twUnit_0.Is(TWTypes.Shield)) twUnit_0.SetShieldProtect(levelTWType);

                            stepUnit_0.TakeSteps();

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWTypeForGive == TWTypes.Pick)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTWType, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTWType);

                                twUnit_0.ToolWeapon = tWTypeForGive;
                                twUnit_0.LevelTWType = levelTWType;

                                stepUnit_0.TakeSteps();

                                RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWTypeForGive == TWTypes.Sword)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, TWTypes.Sword, levelTWType, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, TWTypes.Sword, levelTWType);

                                twUnit_0.ToolWeapon = tWTypeForGive;
                                twUnit_0.LevelTWType = levelTWType;

                                stepUnit_0.TakeSteps();

                                RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWTypeForGive == TWTypes.Shield)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, tWTypeForGive, levelTWType, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, tWTypeForGive, levelTWType);

                                twUnit_0.ToolWeapon = tWTypeForGive;
                                twUnit_0.LevelTWType = levelTWType;
                                twUnit_0.SetShieldProtect(levelTWType);

                                stepUnit_0.TakeSteps();

                                RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcSys.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                    }
                    else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
