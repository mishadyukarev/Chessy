using Chessy.Common;

namespace Chessy.Game
{
    sealed class CenterFriendUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterFriendUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            UIEs.CenterEs.FriendE.ButtonC.SetActiveParent(false);

            if (GameModeC.IsGameMode(GameModes.WithFriendOff))
            {
                if (E.FriendIsActive)
                {
                    UIEs.CenterEs.FriendE.TextC.SetActiveParent(true);

                    if (E.CurPlayerITC.Player == PlayerTypes.First)
                    {
                        UIEs.CenterEs.FriendE.TextC.TextUI.text = "1";
                    }
                    else
                    {
                        UIEs.CenterEs.FriendE.TextC.TextUI.text = "2";
                    }
                }
            }
        }
    }
}
