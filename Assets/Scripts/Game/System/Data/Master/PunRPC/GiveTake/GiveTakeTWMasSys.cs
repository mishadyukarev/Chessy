using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class GiveTakeTWMasSys : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            TWDoingMC.Get(out var tWTypeForGive, out var levelTW);


            if (idx_0 != default)
            {
                var sender = InfoC.Sender(MGOTypes.Master);

                ref var unit_0 = ref Unit<UnitC>(idx_0);

                ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

                ref var stepUnit_0 = ref Unit<StepUnitC>(idx_0);

                ref var tw_0 = ref UnitToolWeapon<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref UnitToolWeapon<LevelC>(idx_0);
                ref var twShield_0 = ref UnitToolWeapon<ProtectionC>(idx_0);


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (stepUnit_0.HaveMin)
                    {

                        if (tw_0.HaveTW)
                        {
                            InvTWC.Add(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Owner);
                            UnitToolWeapon<UnitTWCellC>(idx_0).Reset();

                            stepUnit_0.TakeMin();

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (InvTWC.Have(tWTypeForGive, levelTW, ownUnit_0.Owner))
                        {
                            InvTWC.Take(tWTypeForGive, levelTW, ownUnit_0.Owner);

                            UnitToolWeapon<UnitTWCellC>(idx_0).SetNew(tWTypeForGive, levelTW);

                            stepUnit_0.TakeMin();

                            RpcSys.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWTypeForGive == TWTypes.Pick)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTW, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTW);

                                UnitToolWeapon<UnitTWCellC>(idx_0).SetNew(tWTypeForGive, levelTW);

                                stepUnit_0.TakeMin();

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

                                UnitToolWeapon<UnitTWCellC>(idx_0).SetNew(tWTypeForGive, levelTW);

                                stepUnit_0.TakeMin();

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

                                UnitToolWeapon<UnitTWCellC>(idx_0).SetNew(tWTypeForGive, levelTW);

                                stepUnit_0.TakeMin();

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
