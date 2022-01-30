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

                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;

                ref var levUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;

                ref var tw_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeapon;
                ref var twLevel_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).LevelTW;
                ref var twShield_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).Protection;


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Have)
                    {

                        if (tw_0.HaveTW)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons++;
                            UnitEs.ToolWeapon(idx_0).Reset();

                            UnitEs.StatEs.Step(idx_0).Steps.Take();

                            Es.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (Es.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0.Player).ToolWeapons.Have)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0.Player).ToolWeapons.Take();

                            Es.CellEs.UnitEs.ToolWeapon(idx_0).SetNew(tWForGive, levelTW);

                            Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();

                            Es.Rpc.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWForGive == ToolWeaponTypes.Pick)
                        {
                            if (Es.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                Es.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                Es.CellEs.UnitEs.ToolWeapon(idx_0).SetNew(tWForGive, levelTW);

                                Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();

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

                                Es.CellEs.UnitEs.ToolWeapon(idx_0).SetNew(tWForGive, levelTW);

                                Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();

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

                                Es.CellEs.UnitEs.ToolWeapon(idx_0).SetNew(tWForGive, levelTW);

                                Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();

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
