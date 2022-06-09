﻿using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    sealed class MistakeSs
    {
        internal readonly Chessy.Game.Model.System.MistakeS MistakeS;
        internal readonly SetMistakeS SetMistakeS;

        internal MistakeSs(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            MistakeS = new Chessy.Game.Model.System.MistakeS(sMG, eMG);
            SetMistakeS = new SetMistakeS(sMG, eMG);
        }
    }
}