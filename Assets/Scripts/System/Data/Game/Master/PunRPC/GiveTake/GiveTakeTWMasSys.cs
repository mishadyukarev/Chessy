﻿using static Game.Game.EntityCellPool;

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

                ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

                ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref UnitTW<LevelC>(idx_0);
                ref var twShield_0 = ref UnitTW<ProtectionC>(idx_0);


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (stepUnit_0.HaveMin)
                    {

                        if (tw_0.HaveTW)
                        {
                            InvTWC.Add(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Owner);
                            UnitTW<UnitTWCellEC>(idx_0).Reset();

                            stepUnit_0.TakeMin();

                            RpcS.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (InvTWC.Have(tWTypeForGive, levelTW, ownUnit_0.Owner))
                        {
                            InvTWC.Take(tWTypeForGive, levelTW, ownUnit_0.Owner);

                            UnitTW<UnitTWCellEC>(idx_0).SetNew(tWTypeForGive, levelTW);

                            stepUnit_0.TakeMin();

                            RpcS.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWTypeForGive == TWTypes.Pick)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTW, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, TWTypes.Pick, levelTW);

                                UnitTW<UnitTWCellEC>(idx_0).SetNew(tWTypeForGive, levelTW);

                                stepUnit_0.TakeMin();

                                RpcS.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcS.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWTypeForGive == TWTypes.Sword)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, TWTypes.Sword, levelTW, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, TWTypes.Sword, levelTW);

                                UnitTW<UnitTWCellEC>(idx_0).SetNew(tWTypeForGive, levelTW);

                                stepUnit_0.TakeMin();

                                RpcS.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcS.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWTypeForGive == TWTypes.Shield)
                        {
                            if (InvResC.CanBuyTW(ownUnit_0.Owner, tWTypeForGive, levelTW, out var needRes))
                            {
                                InvResC.BuyTW(ownUnit_0.Owner, tWTypeForGive, levelTW);

                                UnitTW<UnitTWCellEC>(idx_0).SetNew(tWTypeForGive, levelTW);

                                stepUnit_0.TakeMin();

                                RpcS.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                RpcS.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                    }
                    else RpcS.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
