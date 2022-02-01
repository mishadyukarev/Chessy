namespace Game.Game
{
    sealed class GiveTakeToolWeaponMS : SystemCellAbstract, IEcsRunSystem
    {
        public GiveTakeToolWeaponMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var tWForGive = Es.MasterEs.GiveTakeToolWeapon<ToolWeaponTC>().ToolWeapon;
            var levelTW = Es.MasterEs.GiveTakeToolWeapon<LevelTC>().Level;
            var idx_0 = Es.MasterEs.GiveTakeToolWeapon<IdxC>().Idx;


            if (idx_0 != default)
            {
                var sender = InfoC.Sender(MGOTypes.Master);

                var unit_0 = UnitEs(idx_0).MainE.UnitTC;

                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

                var tw_0 = UnitEs(idx_0).ToolWeaponE.ToolWeaponTC;
                var twLevel_0 = UnitEs(idx_0).ToolWeaponE.LevelTC;


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (UnitStatEs(idx_0).StepE.HaveSteps)
                    {

                        if (tw_0.HaveTW)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons.Amount++;
                            UnitEs(idx_0).ToolWeaponE.Reset();

                            UnitStatEs(idx_0).StepE.Take(tWForGive);

                            Es.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (Es.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0.Player).HaveTW)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0.Player).ToolWeapons.Amount--;

                            UnitEs(idx_0).ToolWeaponE.SetNew(tWForGive, levelTW);

                            UnitStatEs(idx_0).StepE.Take(tWForGive);

                            Es.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWForGive == ToolWeaponTypes.Pick)
                        {
                            if (Es.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                Es.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                UnitEs(idx_0).ToolWeaponE.SetNew(tWForGive, levelTW);

                                UnitStatEs(idx_0).StepE.Take(tWForGive);

                                Es.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Sword)
                        {
                            if (Es.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                Es.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                UnitEs(idx_0).ToolWeaponE.SetNew(tWForGive, levelTW);

                                UnitStatEs(idx_0).StepE.Take(tWForGive);

                                Es.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Shield)
                        {
                            if (Es.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                Es.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                UnitEs(idx_0).ToolWeaponE.SetNew(tWForGive, levelTW);

                                UnitStatEs(idx_0).StepE.Take(tWForGive);

                                Es.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                    }
                    else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
