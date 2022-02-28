using UnityEngine;


namespace Chessy.Game
{

    sealed class InputS : SystemAbstract, IEcsRunSystem
    {
        internal InputS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            { 
                E.IsClicked = true;
            }
            else E.IsClicked = false;
        }
    }
}