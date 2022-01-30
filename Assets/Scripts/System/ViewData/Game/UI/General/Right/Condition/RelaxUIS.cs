using UnityEngine;

namespace Game.Game
{
    sealed class RelaxUIS : SystemViewAbstract, IEcsRunSystem
    {
        public RelaxUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var unit_sel = ref Es.CellEs.UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).UnitC;
            ref var selOnUnitCom = ref Es.CellEs.UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).OwnerC;

            ref var selCondUnitC = ref Es.CellEs.UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).ConditionC;


            var activeButt = false;

            if (unit_sel.Have)
            {
                if (selOnUnitCom.Is(Es.WhoseMove.CurPlayerI))
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