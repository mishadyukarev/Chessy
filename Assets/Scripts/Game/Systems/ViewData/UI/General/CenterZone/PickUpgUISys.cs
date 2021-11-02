using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class PickUpgUISys : IEcsRunSystem
    {
        public void Run()
        {
            PickUpgZoneViewUIC.SetActiveZone(PickUpgZoneDataUIC.IsActivated(WhoseMoveC.CurPlayer));

            foreach (var item in PickUpgZoneDataUIC.Activated_Buts)
            {
                PickUpgZoneViewUIC.SetActive_But(item.Key, item.Value);
            }
        }
    }
}