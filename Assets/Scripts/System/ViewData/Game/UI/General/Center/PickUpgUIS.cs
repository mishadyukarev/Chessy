using static Game.Game.CenterUpgradeUIE;

namespace Game.Game
{
    sealed class PickUpgUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal PickUpgUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayer = Es.CurPlayerI.Player;

            var isActivatedZone = Es.AvailableCenterUpgradeEs.HaveUpgrade(curPlayer).Have
                && !Es.ForPlayerE(curPlayer).UnitsInfoE(UnitTypes.King).HaveInInventor;

            Paren.SetActive(isActivatedZone);

            if (isActivatedZone)
            {
                for (var build = BuildingTypes.Farm; build <= BuildingTypes.Woodcutter; build++)
                {
                    if (Es.AvailableCenterUpgradeEs.HaveBuildUpgrade(build, Es.CurPlayerI.Player).Have)
                    {
                        Builds(build).SetActive(true);
                    }
                    else
                    {
                        Builds(build).SetActive(false);
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