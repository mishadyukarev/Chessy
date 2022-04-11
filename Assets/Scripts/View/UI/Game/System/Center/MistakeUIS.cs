using Chessy.Game.Model.Entity;
using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class MistakeUIS : SystemUIAbstract
    {
        readonly MistakeUIE _mistakeUIE;

        readonly bool[] _needActiveMistakeZone = new bool[(byte)MistakeTypes.End];

        internal MistakeUIS(in MistakeUIE mistakeUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _mistakeUIE = mistakeUIE;
        }

        internal override void Sync()
        {
            for (var i = 0; i < _needActiveMistakeZone.Length; i++)
            {
                _needActiveMistakeZone[i] = false;
            }

            foreach (var key in _mistakeUIE.KeysResource)
            {
                _mistakeUIE.NeedAmountResources(key).SetActive(false);
            }


            if (e.MistakeT != MistakeTypes.None)
            {
                if (e.MistakeT == MistakeTypes.Economy)
                {
                    _needActiveMistakeZone[(byte)e.MistakeT] = true;

                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                    {
                        if (e.MistakeEconomy(res).Resources > 0)
                        {
                            _mistakeUIE.NeedAmountResources(res).SetActive(true);

                            _mistakeUIE.NeedAmountResources(res).TextUI.text
                                = res == ResourceTypes.Iron || res == ResourceTypes.Gold ? ">= " + e.MistakeEconomy(res).Resources : ">= " + ((int)(100 * e.MistakeEconomy(res).Resources));
                        }
                    }
                }

                else
                {
                    _needActiveMistakeZone[(byte)e.MistakeT] = true;
                }
            }

            for (var mistakeT = (MistakeTypes)1; mistakeT < MistakeTypes.End; mistakeT++)
            {
                _mistakeUIE.Zones(mistakeT).SetActive(_needActiveMistakeZone[(byte)mistakeT]);
            }
        }
    }
}
