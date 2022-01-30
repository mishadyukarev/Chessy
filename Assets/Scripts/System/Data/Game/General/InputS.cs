using UnityEngine;


namespace Game.Game
{

    sealed class InputS : SystemCellAbstract, IEcsRunSystem
    {
        public InputS(in Entities ents) : base(ents) { }

        public void Run()
        {
            if (Input.GetMouseButtonDown(0)) Es.InputE.IsClickedC.IsClicked = true;
            else Es.InputE.IsClickedC.IsClicked = false;

            Debug.Log(UnitEs.StatEs.Hp(0).Health.Amount);
        }
    }
}