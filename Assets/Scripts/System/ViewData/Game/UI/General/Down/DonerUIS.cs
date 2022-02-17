using UnityEngine;

namespace Game.Game
{
    sealed class DonerUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DonerUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (Es.IsMyMove)
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