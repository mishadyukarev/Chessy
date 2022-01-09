namespace Game.Game
{
    sealed class PickUpgUIS : IEcsRunSystem
    {
        public void Run()
        {

            var isActivatedZone = PickUpgC.HaveUpgrade(WhoseMoveC.CurPlayerI);

            PickUpgUIC.SetActiveZone(isActivatedZone);

            if (isActivatedZone)
            {
                foreach (var item_0 in UnitAvailPickUpgC.Available_1)
                {
                    foreach (var item_1 in item_0.Value)
                    {
                        if (item_1.Key == WhoseMoveC.CurPlayerI)
                        {
                            PickUpgUIC.SetActive(item_0.Key, item_1.Value);
                        }
                    }
                }

                foreach (var item_0 in BuildAvailPickUpgC.Available_1)
                {
                    foreach (var item_1 in item_0.Value)
                    {
                        if (item_1.Key == WhoseMoveC.CurPlayerI)
                        {
                            PickUpgUIC.SetActive(item_0.Key, item_1.Value);
                        }
                    }
                }


                foreach (var item_0 in WaterAvailPickUpgC.Available)
                {
                    if (item_0.Key == WhoseMoveC.CurPlayerI)
                    {
                        PickUpgUIC.SetWater(item_0.Value);
                    }
                }
            }
        }
    }
}