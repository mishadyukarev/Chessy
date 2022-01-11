using static Game.Game.EntityCenterPickUpgUIPool;

namespace Game.Game
{
    struct PickUpgUIS : IEcsRunSystem
    {
        public void Run()
        {
            var isActivatedZone = PickUpgC.HaveUpgrade(WhoseMoveC.CurPlayerI);

            Water<ButtonVC>().SetActiveParent(isActivatedZone);

            if (isActivatedZone)
            {
                foreach (var item_0 in UnitAvailPickUpgC.Available_1)
                {
                    foreach (var item_1 in item_0.Value)
                    {
                        if (item_1.Key == WhoseMoveC.CurPlayerI)
                        {
                            Units<ButtonVC>(item_0.Key).SetActive(item_1.Value);
                        }
                    }
                }

                foreach (var item_0 in BuildAvailPickUpgC.Available_1)
                {
                    foreach (var item_1 in item_0.Value)
                    {
                        if (item_1.Key == WhoseMoveC.CurPlayerI)
                        {
                            Builds<ButtonVC>(item_0.Key).SetActive(item_1.Value);
                        }
                    }
                }


                foreach (var item_0 in WaterAvailPickUpgC.Available)
                {
                    if (item_0.Key == WhoseMoveC.CurPlayerI)
                    {
                        Water<ButtonVC>().SetActive(item_0.Value);
                    }
                }
            }
        }
    }
}