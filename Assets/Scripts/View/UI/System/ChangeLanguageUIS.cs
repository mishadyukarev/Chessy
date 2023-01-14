using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    internal sealed class ChangeLanguageUIS : SystemAbstract
    {
        readonly EntitiesViewUI _eUI;

        internal ChangeLanguageUIS(in EntitiesViewUI entsUI, in EntitiesModel eM) : base(eM)
        {
            _eUI = entsUI;
        }

        internal void ChangeLanguage(in LanguageTypes languageT)
        {
            _eUI.UpEs.LeaveTextC.TextUI.text = "s";
        }
    }
}