﻿namespace Chessy.Game.Entity.Model
{
    public struct MistakeE
    {
        public MistakeTC MistakeTC;
        public TimerC TimerC;

        public void Set(in MistakeTypes mistakeT, in float timer)
        {
            MistakeTC.Mistake = mistakeT;
            TimerC.Timer = timer;
        }
    }
}
