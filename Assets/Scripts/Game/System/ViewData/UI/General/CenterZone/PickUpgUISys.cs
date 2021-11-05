using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class PickUpgUISys : IEcsRunSystem
    {
        public void Run()
        {
            var isActivatedZone = PickUpgZoneDataUIC.HaveUpgrade(WhoseMoveC.CurPlayerI);

            PickUpgZoneViewUIC.SetActiveZone(isActivatedZone);

            if (isActivatedZone)
            {
                foreach (var item_0 in PickUpgZoneDataUIC.Activated_Buts)
                {
                    if (item_0.Key == WhoseMoveC.CurPlayerI)
                    {
                        foreach (var item_1 in item_0.Value)
                        {
                            PickUpgZoneViewUIC.SetActive_But(item_1.Key, item_1.Value);
                        }
                    }  
                }
            }
        }
    }
}