using Chessy.Model;

namespace Chessy.Model
{
    sealed class MotionUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI _eUI;

        bool _needActive;

        internal MotionUIS(in EntitiesViewUI eUI, in EntitiesModel eMG) : base(eMG)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            if (_e.MotionTimer > 0)
            {
                _needActive = true;
                _eUI.CenterEs.MotionTextC.TextUI.text = _e.Motions.ToString();
            }
            else
            {
                _needActive = false;
            }

            _eUI.CenterEs.MotionTextC.SetActiveParent(_needActive);
        }
    }
}