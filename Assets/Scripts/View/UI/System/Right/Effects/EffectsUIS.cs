using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
namespace Chessy.Model
{
    sealed class EffectsUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI _eUI;

        readonly bool[] _needActiveButton = new bool[(byte)ButtonTypes.End];

        internal EffectsUIS(in EntitiesViewUI eUI, in EntitiesModel eMG) : base(eMG)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            var needActiveZone = false;

            if (!aboutGameC.LessonType.HaveLesson() || aboutGameC.LessonType >= Enum.LessonTypes.UniqueAttackInfo)
            {
                if (indexesCellsC.IsSelectedCell)
                {
                    var idx_sel = indexesCellsC.Selected;

                    if (unitCs[indexesCellsC.Selected].HaveUnit)
                    {
                        needActiveZone = true;

                        for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                        {
                            _needActiveButton[(byte)buttonT] = _effectsUnitsRightBarsCs[idx_sel].Effect(buttonT) != EffectTypes.None;

                            if (_needActiveButton[(byte)buttonT])
                            {
                                _eUI.RightEs.Effect(buttonT).ImageC.Image.sprite = fromResourcesC.Sprite(_effectsUnitsRightBarsCs[idx_sel].Effect(buttonT));
                            }

                            _eUI.RightEs.Effect(buttonT).GO.TrySetActive(_needActiveButton[(byte)buttonT]);
                        }
                    }
                }
            }

            _eUI.RightEs.Effect(ButtonTypes.First).GO.Transform.parent.gameObject.SetActive(needActiveZone);
        }
    }
}