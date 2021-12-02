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
            TWDoingMC.Get(out var tWTypeForGive, out var levelTW);


            if (idx_0 != default)
            {
                var sender = InfoC.Sender(MGOTypes.Master);

                ref var unit_0 = ref _unitF.Get1(idx_0);

                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

                ref var tw_0 = ref EntityPool.ToolWeapon<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref EntityPool.ToolWeapon<LevelC>(idx_0);
                ref var twShield_0 = ref EntityPool.ToolWeapon<ShieldC>(idx_0);


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (stepUnit_0.HaveMin)
                    {

                        if (tw_0.HaveTW)
                        {
                            InvTWC.Add(tw_0.TW, twLevel_0.Level, ownUnit_0.Owner);
                            tw_0.Reset();

                            stepUnit_0.TakeSteps();

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (InvTWC.Have(tWTypeForGive, levelTW, ownUnit_0.Owner))
                        {
                            InvTWC.Take(tWTypeForGive, levelTW, ownUnit_0.Owner);

                            tw_0.Set(tWTypeForGive);
                            twLevel_0.Set(levelTW);
                            if (tw_0.Is(TWTypes.Shield)) twShield_0.SetShieldProtect(levelTW);

                            stepUnit_0.TakeSteps();

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWTypeForGive == TWTypes.Pick)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTW, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTW);

                                tw_0.Set(tWTypeForGive);
                                twLevel_0.Set(levelTW);

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
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, TWTypes.Sword, levelTW, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, TWTypes.Sword, levelTW);

                                tw_0.Set(tWTypeForGive);
                                twLevel_0.Set(levelTW);

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
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, tWTypeForGive, levelTW, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, tWTypeForGive, levelTW);

                                tw_0.Set(tWTypeForGive);
                                twLevel_0.Set(levelTW);
                                twShield_0.SetShieldProtect(levelTW);

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
