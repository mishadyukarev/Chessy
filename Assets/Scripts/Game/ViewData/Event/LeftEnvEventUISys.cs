﻿using System;

namespace Game.Game
{
    sealed class LeftEnvEventUISys : SystemUIAbstract
    {
        readonly Action _updateView;

        internal LeftEnvEventUISys(in Action updateView, in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _updateView = updateView;
            UIEs.LeftEs.EnvironmentEs.Info<ButtonUIC>().AddListener(EnvironmentInfo);
        }

        void EnvironmentInfo()
        {
            E.EnvIsActive = !E.EnvIsActive;
            _updateView.Invoke();
        }
    }
}