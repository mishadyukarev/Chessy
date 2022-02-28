using System;

namespace Chessy.Game
{
    sealed class LeftEnvironmentEventUIS : SystemUIAbstract
    {
        readonly Action _updateView;

        internal LeftEnvironmentEventUIS(in Action updateView, in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _updateView = updateView;
            UIEs.LeftEs.EnvironmentEs.InfoButtonC.AddListener(EnvironmentInfo);
        }

        void EnvironmentInfo()
        {
            E.EnvIsActive = !E.EnvIsActive;
            _updateView.Invoke();
        }
    }
}