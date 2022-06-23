﻿using Chessy.Common;
using Chessy.Model.Model.Entity;

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

            if (_e.GameModeT.Is(GameModeTypes.WithFriendOffline))
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