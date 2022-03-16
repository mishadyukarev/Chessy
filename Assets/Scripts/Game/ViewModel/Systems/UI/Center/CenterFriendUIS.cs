using Chessy.Common;

namespace Chessy.Game
{
    sealed class CenterFriendUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterFriendUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            UIE.CenterEs.FriendE.ButtonC.SetActiveParent(false);

            if (GameModeC.IsGameMode(GameModes.WithFriendOff))
            {
                if (E.FriendIsActive)
                {
                    UIE.CenterEs.FriendE.TextC.SetActiveParent(true);

                    if (E.CurPlayerITC.Player == PlayerTypes.First)
                    {
                        UIE.CenterEs.FriendE.TextC.TextUI.text = "1";
                    }
                    else
                    {
                        UIE.CenterEs.FriendE.TextC.TextUI.text = "2";
                    }
                }
            }
        }
    }
}
