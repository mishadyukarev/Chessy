using Game.Common;
using static Game.Game.CenterFriendUIE;

namespace Game.Game
{
    sealed class FriendZoneUISys : SystemUIAbstract, IEcsRunSystem
    {
        internal FriendZoneUISys(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            ButtonC.SetActiveParent(false);

            if (GameModeC.IsGameMode(GameModes.WithFriendOff))
            {
                if (Es.FriendIsActive)
                {
                    TextC.SetActiveParent(true);

                    if (Es.CurPlayerI.Player == PlayerTypes.First)
                    {
                        TextC.Text = "1";
                    }
                    else
                    {
                        TextC.Text = "2";
                    }
                }
            }
        }
    }
}
