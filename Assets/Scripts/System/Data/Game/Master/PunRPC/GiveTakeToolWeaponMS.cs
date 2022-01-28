﻿using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct GiveTakeToolWeaponMS : IEcsRunSystem
    {
        public void Run()
        {
            var tWForGive = Entities.MasterEs.GiveTakeToolWeapon<ToolWeaponC>().ToolWeapon;
            var levelTW = Entities.MasterEs.GiveTakeToolWeapon<LevelTC>().Level;
            var idx_0 = Entities.MasterEs.GiveTakeToolWeapon<IdxC>().Idx;


            if (idx_0 != default)
            {
                var sender = InfoC.Sender(MGOTypes.Master);

                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;

                ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

                ref var tw_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeaponC;
                ref var twLevel_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).LevelC;
                ref var twShield_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).Protection;


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
                    {

                        if (tw_0.HaveTW)
                        {
                            InventorToolWeaponE.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player)++;
                            Entities.CellEs.UnitEs.Reset(idx_0);

                            Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();

                            Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (InventorToolWeaponE.ToolWeapons<AmountC>(tWForGive, levelTW, ownUnit_0.Player).Have)
                        {
                            InventorToolWeaponE.ToolWeapons<AmountC>(tWForGive, levelTW, ownUnit_0.Player).Take();

                            Entities.CellEs.UnitEs.SetNew(idx_0, tWForGive, levelTW);

                            Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();

                            Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWForGive == ToolWeaponTypes.Pick)
                        {
                            if (InventorResourcesE.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                InventorResourcesE.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                Entities.CellEs.UnitEs.SetNew(idx_0, tWForGive, levelTW);

                                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();

                                Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Sword)
                        {
                            if (InventorResourcesE.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                InventorResourcesE.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                Entities.CellEs.UnitEs.SetNew(idx_0, tWForGive, levelTW);

                                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();

                                Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Shield)
                        {
                            if (InventorResourcesE.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                InventorResourcesE.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                Entities.CellEs.UnitEs.SetNew(idx_0, tWForGive, levelTW);

                                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();

                                Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                    }
                    else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
