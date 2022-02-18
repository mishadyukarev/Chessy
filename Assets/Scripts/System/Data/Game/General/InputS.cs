using UnityEngine;


namespace Game.Game
{

    sealed class InputS : SystemAbstract, IEcsRunSystem
    {
        internal InputS(in Entities ents) : base(ents)
        {

        }

        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            { 
                Es.IsClicked = true;
            }
            else Es.IsClicked = false;
        }
    }
}