using Chessy.Game.Entity.View.UI.Down;
using UnityEngine;

namespace Chessy.Game
{
    sealed class DonerUIS : SystemAbstract, IEcsRunSystem
    {
        readonly DonerUIE _donerE;

        internal DonerUIS(in DonerUIE downDoner, in EntitiesModel ents) : base(ents)
        {
            _donerE = downDoner;
        }

        public void Run()
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                _donerE.WaitGoC.SetActive(false);
                _donerE.ButtonC.Image.color = Color.white;
            }
            else
            {
                _donerE.WaitGoC.SetActive(true);
                _donerE.ButtonC.Image.color = Color.red;
            }
        }
    }
}