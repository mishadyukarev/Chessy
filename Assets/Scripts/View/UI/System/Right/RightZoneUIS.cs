using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.Model
{
    sealed class RightZoneUIS : SystemUIAbstract
    {
        bool _needAppear;
        bool _wasApeared;
        readonly Animation _animation;

        const string APPEAR_NAME = "RightZoneAppearUI";
        const string DISAPPEAR_NAME = "RightZoneDisappearUI";

        internal RightZoneUIS(in EntitiesViewUI eUI, in EntitiesModel ents) : base(ents)
        {
            var go = eUI.RightEs.Zone.GO;

            _animation = go.GetComponent<Animation>();
            _animation.Play(DISAPPEAR_NAME);
        }

        internal override void Sync()
        {
            var idx_sel = _e.SelectedCellIdx;

            _needAppear = false;


            if (_e.SelectedCellIdx > 0)
            {
                if (_unitCs[idx_sel].UnitType.HaveUnit())
                {
                    if (_unitVisibleCs[idx_sel].IsVisible(_aboutGameC.CurrentPlayerIType))
                    {
                        _needAppear = true;
                    }
                }
            }


            if(_wasApeared != _needAppear)
            {
                _animation.Play(_needAppear ? APPEAR_NAME : DISAPPEAR_NAME);
            }

            _wasApeared = _needAppear;
        }
    }
}