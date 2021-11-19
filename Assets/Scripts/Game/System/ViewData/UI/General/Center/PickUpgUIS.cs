using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class PickUpgUIS : IEcsRunSystem
    {
        public void Run()
        {
           
            var isActivatedZone = PickUpgC.HaveUpgrade(WhoseMoveC.CurPlayerI);

            PickUpgUIC.SetActiveZone(isActivatedZone);

            if (isActivatedZone)
            {
                //foreach (var item_0 in PickUpgC.Activated_Buts)
                //{
                //    if (item_0.Key == WhoseMoveC.CurPlayerI)
                //    {
                //        //foreach (var item_1 in item_0.Value)
                //        //{
                //        //    PickUpgUIC.SetActive_But(item_1.Key, item_1.Value);
                //        //}
                //    }  
                //}
            }
        }
    }
}