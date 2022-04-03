using Chessy.Game.Model.Entity;

namespace Chessy.Game.View.UI
{
    sealed class MotionUpUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly TextUIC _motionTextC;

        internal MotionUpUIS(in TextUIC motionTextC, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _motionTextC = motionTextC;
        }

        public void Run()
        {
            if (e.LessonTC.HaveLesson)
            {
                _motionTextC.GameObject.SetActive(false);
            }
            else
            {
                _motionTextC.Parent_T.gameObject.SetActive(true);

                _motionTextC.TextUI.text = "Motions: " + e.MotionsC.Motions.ToString();
            }
        }
    }
}