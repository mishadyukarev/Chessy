using UnityEngine;

namespace Chessy.Game
{
    sealed class DonerUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DonerUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            //if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            //{
            //    UIEntDownDoner.Wait<GameObjectVC>().SetActive(false);
            //    UIEntDownDoner.Doner<ButtonUIC>().Color = Color.white;
            //}
            //else
            //{
            //    UIEntDownDoner.Wait<GameObjectVC>().SetActive(true);
            //    UIEntDownDoner.Doner<ButtonUIC>().Color = Color.red;
            //}
        }
    }
}