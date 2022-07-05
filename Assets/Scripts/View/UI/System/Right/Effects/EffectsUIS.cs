using Chessy.Model.Entity;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class EffectsUIS : SystemUIAbstract
    {
        readonly FromResourcesC _resourcesE;
        readonly EntitiesViewUI _eUI;

        readonly bool[] _needActiveButton = new bool[(byte)ButtonTypes.End];

        internal EffectsUIS(in FromResourcesC resources, in EntitiesViewUI eUI, in EntitiesModel eMG) : base(eMG)
        {
            _resourcesE = resources;
            _eUI = eUI;
        }

        internal override void Sync()
        {
            var needActiveZone = false;

            if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.ThatsYourEffects)
            {
                if (_e.CellsC.IsSelectedCell)
                {
                    var idx_sel = _e.SelectedCellIdx;

                    if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                    {
                        needActiveZone = true;

                        for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                        {
                            _needActiveButton[(byte)buttonT] = _e.EffectsUnitsRightBarsC(idx_sel).Effect(buttonT) != EffectTypes.None;

                            if (_needActiveButton[(byte)buttonT])
                            {
                                _eUI.RightEs.Effect(buttonT).ImageC.Image.sprite = _resourcesE.Sprite(_e.EffectsUnitsRightBarsC(idx_sel).Effect(buttonT));
                            }

                            _eUI.RightEs.Effect(buttonT).GO.SetActive(_needActiveButton[(byte)buttonT]);
                        }
                    }
                }
            }

            _eUI.RightEs.Effect(ButtonTypes.First).GO.Transform.parent.gameObject.SetActive(needActiveZone);
        }
    }
}