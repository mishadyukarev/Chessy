using Game.Common;
using UnityEngine;

namespace Game.Game
{
    struct DonerUIS : IEcsRunSystem
    {
        public void Run()
        {
            if (EntWhoseMove.IsMyMove)
            {
                UIEntDownDoner.Wait<GameObjectVC>().SetActive(false);
                UIEntDownDoner.Doner<ButtonUIC>().Color = Color.white;
            }
            else
            {
                UIEntDownDoner.Wait<GameObjectVC>().SetActive(true);
                UIEntDownDoner.Doner<ButtonUIC>().Color = Color.red;
            }
        }
    }
}