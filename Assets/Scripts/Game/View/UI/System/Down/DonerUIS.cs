using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.View.UI.Down;
using UnityEngine;

namespace Chessy.Game
{
    sealed class DonerUIS : SystemModelGameAbs, IEcsRunSystem
    {
        readonly DonerUIE _donerE;

        internal DonerUIS(in DonerUIE downDoner, in EntitiesModelGame ents) : base(ents)
        {
            _donerE = downDoner;
        }

        public void Run()
        {
            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
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