using static Game.Game.CenterUpgradeUIE;

namespace Game.Game
{
    sealed class PickUpgUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal PickUpgUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayer = E.CurPlayerI.Player;

            var isActivatedZone = E.PlayerE(curPlayer).HaveCenterUpgrade && !E.UnitInfo(curPlayer, LevelTypes.First, UnitTypes.King).HaveInInventor;

            Paren.SetActive(isActivatedZone);

            if (isActivatedZone)
            {
                //for (var buildT = BuildingTypes.Farm; buildT <= BuildingTypes.Woodcutter; buildT++)
                //{
                //    Builds(buildT).SetActive(E.BuildingsInfo(E.CurPlayerI.Player, LevelTypes.First, buildT).HaveCenterUpgrade);
                //}

                //for (var unitT = UnitTypes.King; unitT <= UnitTypes.Pawn; unitT++)
                //{
                //    Units(unitT).SetActive(E.UnitInfo(E.CurPlayerI.Player, LevelTypes.First, unitT).HaveCenterUpgrade);
                //}


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