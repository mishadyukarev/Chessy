using UnityEngine;

namespace Game.Game
{
    sealed class DonerUIS : SystemViewAbstract, IEcsRunSystem
    {
        public DonerUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            if (Es.WhoseMoveE.IsMyMove)
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