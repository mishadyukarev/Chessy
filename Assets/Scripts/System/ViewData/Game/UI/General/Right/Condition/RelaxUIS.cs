﻿using UnityEngine;

namespace Game.Game
{
    sealed class RelaxUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RelaxUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var unit_sel = UnitEs(idx_sel).TypeE.UnitTC;
            var selOnUnitCom = UnitEs(idx_sel).OwnerE.OwnerC;


            var activeButt = false;

            if (UnitEs(idx_sel).TypeE.HaveUnit)
            {
                if (selOnUnitCom.Is(Es.WhoseMoveE.CurPlayerI))
                {
                    activeButt = true;

                    if (UnitEs(idx_sel).ConditionE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.green;
                    }
                    else
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.white;
                    }

                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);

                    RightRelaxUIE.Button<GameObjectVC>(unit_sel.Unit).SetActive(true);
                }
            }

            RightRelaxUIE.Button<ButtonUIC>().SetActive(activeButt);
        }
    }
}