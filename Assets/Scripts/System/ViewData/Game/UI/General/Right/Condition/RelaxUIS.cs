using UnityEngine;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct RelaxUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit<UnitTC>(SelIdx<IdxC>().Idx);
            ref var selOnUnitCom = ref Unit<PlayerTC>(SelIdx<IdxC>().Idx);

            ref var selCondUnitC = ref Unit<ConditionUnitC>(SelIdx<IdxC>().Idx);


            var activeButt = false;

            if (unit_sel.Have)
            {
                if (selOnUnitCom.Is(WhoseMoveE.CurPlayerI))
                {
                    activeButt = true;

                    if (selCondUnitC.Is(ConditionUnitTypes.Relaxed))
                    {
                        UIEntRelax.Button<ImageUIC>().Color = Color.green;
                    }
                    else
                    {
                        UIEntRelax.Button<ImageUIC>().Color = Color.white;
                    }

                    UIEntRelax.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    UIEntRelax.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    UIEntRelax.Button<GameObjectVC>(UnitTypes.Archer).SetActive(false);
                    UIEntRelax.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);
                    UIEntRelax.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);

                    UIEntRelax.Button<GameObjectVC>(unit_sel.Unit).SetActive(true);
                }
            }

            UIEntRelax.Button<ButtonUIC>().SetActive(activeButt);
        }
    }
}