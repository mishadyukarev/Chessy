﻿using System;

namespace Chessy.Game
{
    sealed class LeftEnvironmentEventUIS : SystemUIAbstract
    {
        readonly Action _updateView;

        internal LeftEnvironmentEventUIS(in Action updateView,  in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
            _updateView = updateView;
            UIE.LeftEs.EnvironmentEs.InfoButtonC.AddListener(EnvironmentInfo);
        }

        void EnvironmentInfo()
        {
            E.EnvIsActive = !E.EnvIsActive;
            _updateView.Invoke();
        }
    }
}