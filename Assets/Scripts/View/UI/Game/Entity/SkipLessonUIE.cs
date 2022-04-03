using UnityEngine.UI;

namespace Chessy.Game.View.UI.Entity
{
    public readonly struct SkipLessonUIE
    {
        public readonly ButtonUIC ButtonUIC;

        internal SkipLessonUIE(in Button button)
        {
            ButtonUIC = new ButtonUIC(button);
        }
    }
}