using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class PickUpgUISys : IEcsRunSystem
    {
        public void Run()
        {
            var isActivatedZone = PickUpgZoneDataUIC.IsActivated(WhoseMoveC.CurPlayerI);

            PickUpgZoneViewUIC.SetActiveZone(isActivatedZone);

            if (isActivatedZone)
            {
                foreach (var item in PickUpgZoneDataUIC.Activated_Buts(WhoseMoveC.CurPlayerI))
                {
                    PickUpgZoneViewUIC.SetActive_But(item.Key, item.Value);
                }
            }
        }
    }
}