using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class MotionUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame _eUI;

        internal MotionUIS(in EntitiesViewUIGame eUI, in EntitiesModelGame eMG) : base(eMG)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            if (e.MotionTimer > 0)
            {
                _eUI.CenterEs.Motion.SetActiveParent(false);

                _eUI.CenterEs.Motion.TextUI.text = e.MotionsC.Motions.ToString();
                _eUI.CenterEs.Motion.SetActiveParent(true);
            }
            else
            {
                _eUI.CenterEs.Motion.SetActiveParent(false);
            }
        }
    }
}