using Chessy.Model.Entity;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class MistakeUIS : SystemUIAbstract
    {
        readonly MistakeUIE _mistakeUIE;

        readonly bool[] _needActiveMistakeZone = new bool[(byte)MistakeTypes.End];

        internal MistakeUIS(in MistakeUIE mistakeUIE, in EntitiesModel eMG) : base(eMG)
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


            if (_e.MistakeT != MistakeTypes.None)
            {
                if (_e.MistakeT == MistakeTypes.Economy)
                {
                    _needActiveMistakeZone[(byte)_e.MistakeT] = true;

                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                    {
                        if (_e.MistakeEconomy(res).ResourcesP > 0)
                        {
                            _mistakeUIE.NeedAmountResources(res).SetActive(true);

                            _mistakeUIE.NeedAmountResources(res).TextUI.text
                                = res == ResourceTypes.Iron || res == ResourceTypes.Gold ? ">= " + _e.MistakeEconomy(res).ResourcesP : ">= " + ((int)(100 * _e.MistakeEconomy(res).ResourcesP));
                        }
                    }
                }

                else
                {
                    _needActiveMistakeZone[(byte)_e.MistakeT] = true;
                }
            }

            for (var mistakeT = (MistakeTypes)1; mistakeT < MistakeTypes.End; mistakeT++)
            {
                _mistakeUIE.Zones(mistakeT).TrySetActive(_needActiveMistakeZone[(byte)mistakeT]);
            }
        }
    }
}
