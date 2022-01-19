using static Game.Game.CenterUpgradeUIE;

namespace Game.Game
{
    struct PickUpgUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveE.CurPlayerI;

            var isActivatedZone = AvailableCenterUpgradeEs.HaveUpgrade<HaveUpgradeC>(curPlayer).Have
                && !InventorUnitsE.Units<AmountC>(UnitTypes.King, LevelTypes.First, curPlayer).Have;

            Water<ButtonUIC>().SetActiveParent(isActivatedZone);

            if (isActivatedZone)
            {
                for (var build = BuildingTypes.Farm; build <= BuildingTypes.Mine; build++)
                {
                    if (AvailableCenterUpgradeEs.HaveBuildUpgrade<HaveUpgradeC>(build, WhoseMoveE.CurPlayerI).Have)
                    {
                        Builds<ButtonUIC>(build).SetActive(true);
                    }
                    else
                    {
                        Builds<ButtonUIC>(build).SetActive(false);
                    }
                }

                //foreach (var item_0 in AvailableUpgradeEs.Keys UnitAvailPickUpgC.Available_1)
                //{
                //    foreach (var item_1 in item_0.Value)
                //    {
                //        if (item_1.Key == WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI)
                //        {
                //            Units<ButtonUIC>(item_0.Key).SetActive(item_1.Value);
                //        }
                //    }
                //}

                //foreach (var item_0 in BuildAvailPickUpgC.Available_1)
                //{
                //    foreach (var item_1 in item_0.Value)
                //    {
                //        if (item_1.Key == WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI)
                //        {
                //            Builds<ButtonUIC>(item_0.Key).SetActive(item_1.Value);
                //        }
                //    }
                //}


                //foreach (var item_0 in WaterAvailPickUpgC.Available)
                //{
                //    if (item_0.Key == WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI)
                //    {
                //        Water<ButtonUIC>().SetActive(item_0.Value);
                //    }
                //}
            }
        }
    }
}