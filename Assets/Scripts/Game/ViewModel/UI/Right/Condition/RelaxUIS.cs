using Chessy.Game.Entity.View.UI.Right;
using UnityEngine;

namespace Chessy.Game
{
    sealed class RelaxUIS : SystemAbstract, IEcsRunSystem
    {
        readonly RelaxUIE _relaxE;

        internal RelaxUIS(in RelaxUIE relaxUIE, in EntitiesModel ents) : base(ents)
        {
            _relaxE = relaxUIE;
        }

        public void Run()
        {
            var idx_0 = E.SelectedIdxC.Idx;

            var activeButt = false;

            if (E.UnitTC(idx_0).HaveUnit)
            {
                if (E.UnitPlayerTC(idx_0).Is(E.CurPlayerITC.Player))
                {
                    activeButt = true;

                    _relaxE.ImageUIC.Image.color = E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed) ? Color.green : Color.white;

                    for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                    {
                        _relaxE.Button(unitT).SetActive(false);
                    }

                    if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                    {
                        if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                        {
                            _relaxE.Button(E.UnitTC(idx_0).Unit).SetActive(true);
                        }
                        else
                        {
                            _relaxE.Button(E.UnitTC(idx_0).Unit).SetActive(false);
                        }
                    }
                    else
                    {
                        _relaxE.Button(E.UnitTC(idx_0).Unit).SetActive(true);
                    }
                }
            }

            _relaxE.ButtonC.Button.gameObject.SetActive(activeButt);
        }
    }
}