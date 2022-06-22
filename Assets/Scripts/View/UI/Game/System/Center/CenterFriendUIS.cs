using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.System;

namespace Chessy.Game
{
    sealed class CenterFriendUIS : SystemUIAbstract
    {
        readonly EntitiesModelCommon _eMC;
        readonly EntitiesViewUIGame _eUI;

        bool _needActive;

        internal CenterFriendUIS(in EntitiesModelCommon eMC, in EntitiesViewUIGame eUI, in EntitiesModelGame eMG) : base(eMG)
        {
            _eMC = eMC;
            _eUI = eUI;
        }

        internal override void Sync()
        {
            _needActive = false;

            if (_eMC.GameModeT.Is(GameModeTypes.WithFriendOffline))
            {
                if (_e.ZoneInfoC.IsActiveFriend)
                {
                    _needActive = true;

                    if (_e.CurPlayerIT == PlayerTypes.First)
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
