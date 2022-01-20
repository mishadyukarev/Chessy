﻿using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellUnitTWE;

namespace Game.Game
{
    struct GiveTakeToolWeaponMS : IEcsRunSystem
    {
        public void Run()
        {
            var tWForGive = EntityMPool.GiveTakeToolWeapon<ToolWeaponC>().ToolWeapon;
            var levelTW = EntityMPool.GiveTakeToolWeapon<LevelTC>().Level;
            var idx_0 = EntityMPool.GiveTakeToolWeapon<IdxC>().Idx;


            if (idx_0 != default)
            {
                var sender = InfoC.Sender(MGOTypes.Master);

                ref var unit_0 = ref Unit(idx_0);

                ref var levUnit_0 = ref CellUnitElseEs.Level(idx_0);
                ref var ownUnit_0 = ref CellUnitElseEs.Owner(idx_0);

                ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref UnitTW<LevelTC>(idx_0);
                ref var twShield_0 = ref UnitTW<ProtectionC>(idx_0);


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (CellUnitStepEs.HaveMin(idx_0))
                    {

                        if (tw_0.HaveTW)
                        {
                            InventorToolWeaponE.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).Add();
                            CellUnitTWE.Reset(idx_0);

                            CellUnitStepEs.TakeMin(idx_0);

                            EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (InventorToolWeaponE.ToolWeapons<AmountC>(tWForGive, levelTW, ownUnit_0.Player).Have)
                        {
                            InventorToolWeaponE.ToolWeapons<AmountC>(tWForGive, levelTW, ownUnit_0.Player).Take();

                            CellUnitTWE.SetNew(idx_0, tWForGive, levelTW);

                            CellUnitStepEs.TakeMin(idx_0);

                            EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWForGive == ToolWeaponTypes.Pick)
                        {
                            if (InventorResourcesE.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                InventorResourcesE.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                CellUnitTWE.SetNew(idx_0, tWForGive, levelTW);

                                CellUnitStepEs.TakeMin(idx_0);

                                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Sword)
                        {
                            if (InventorResourcesE.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                InventorResourcesE.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                CellUnitTWE.SetNew(idx_0, tWForGive, levelTW);

                                CellUnitStepEs.TakeMin(idx_0);

                                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Shield)
                        {
                            if (InventorResourcesE.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                InventorResourcesE.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                CellUnitTWE.SetNew(idx_0, tWForGive, levelTW);

                                CellUnitStepEs.TakeMin(idx_0);

                                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                    }
                    else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
