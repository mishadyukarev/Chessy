using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
namespace Chessy.Model
{
    sealed class CenterFriendUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI _eUI;

        bool _needActive;

        internal CenterFriendUIS(in EntitiesViewUI eUI, in EntitiesModel eMG) : base(eMG)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            _needActive = false;

            if (AboutGameC.GameModeType.Is(GameModeTypes.WithFriendOffline))
            {
                if (_zonesInfoC.IsActiveFriend)
                {
                    _needActive = true;

                    if (AboutGameC.CurrentPlayerIType == PlayerTypes.First)
                    {
                        _eUI.CenterEs.FriendE.TextC.TextUI.text = "1";
                    }
                    else
                    {
                        _eUI.CenterEs.FriendE.TextC.TextUI.text = "2";
                    }
                }
            }

            _eUI.CenterEs.FriendE.ButtonC.SetActiveParent(_needActive);
        }
    }
}
