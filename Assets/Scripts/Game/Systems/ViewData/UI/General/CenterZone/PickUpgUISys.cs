using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class PickUpgUISys : IEcsRunSystem
    {
        public void Run()
        {
            var isActivatedZone = PickUpgZoneDataUIC.IsActivated(WhoseMoveC.CurPlayer);

            PickUpgZoneViewUIC.SetActiveZone(isActivatedZone);

            if (isActivatedZone)
            {
                foreach (var item in PickUpgZoneDataUIC.Activated_Buts(WhoseMoveC.CurPlayer))
                {
                    PickUpgZoneViewUIC.SetActive_But(item.Key, item.Value);
                }
            }
        }
    }
}