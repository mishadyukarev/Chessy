using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class CenterFriendUIS : SystemUIAbstract
    {
        readonly EntitiesModelCommon _eMC;
        readonly EntitiesViewUIGame _eUI;

        internal CenterFriendUIS(in EntitiesModelCommon eMC, in EntitiesViewUIGame eUI, in EntitiesModelGame eMG) : base(eMG)
        {
            _eMC = eMC;
            _eUI = eUI;
        }

        internal override void Sync()
        {
            _eUI.CenterEs.FriendE.ButtonC.SetActiveParent(false);

            if (_eMC.GameModeTC.Is(GameModeTypes.WithFriendOffline))
            {
                if (e.ZoneInfoC.IsActiveFriend)
                {
                    _eUI.CenterEs.FriendE.TextC.SetActiveParent(true);

                    if (e.CurPlayerITC.PlayerT == PlayerTypes.First)
                    {
                        _eUI.CenterEs.FriendE.TextC.TextUI.text = "1";
                    }
                    else
                    {
                        _eUI.CenterEs.FriendE.TextC.TextUI.text = "2";
                    }
                }
            }
        }
    }
}
