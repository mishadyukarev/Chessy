using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class MotionUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame _eUI;

        bool _needActive;

        internal MotionUIS(in EntitiesViewUIGame eUI, in EntitiesModelGame eMG) : base(eMG)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            if (e.MotionTimer > 0)
            {
                _needActive = true;
                _eUI.CenterEs.MotionTextC.TextUI.text = e.MotionsC.Motions.ToString();
            }
            else
            {
                _needActive = false;
            }

            _eUI.CenterEs.MotionTextC.SetActiveParent(_needActive);
        }
    }
}