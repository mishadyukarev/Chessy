using UnityEngine;

namespace Game.Game
{
    struct RelaxUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref CellUnitEntities.Else(EntitiesPool.SelectedIdxE.IdxC.Idx).UnitC;
            ref var selOnUnitCom = ref CellUnitEntities.Else(EntitiesPool.SelectedIdxE.IdxC.Idx).OwnerC;

            ref var selCondUnitC = ref CellUnitEntities.Else(EntitiesPool.SelectedIdxE.IdxC.Idx).ConditionC;


            var activeButt = false;

            if (unit_sel.Have)
            {
                if (selOnUnitCom.Is(WhoseMoveE.CurPlayerI))
                {
                    activeButt = true;

                    if (selCondUnitC.Is(ConditionUnitTypes.Relaxed))
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.green;
                    }
                    else
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.white;
                    }

                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Archer).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);

                    RightRelaxUIE.Button<GameObjectVC>(unit_sel.Unit).SetActive(true);
                }
            }

            RightRelaxUIE.Button<ButtonUIC>().SetActive(activeButt);
        }
    }
}