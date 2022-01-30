using Game.Common;
using static Game.Game.CenterFriendUIE;

namespace Game.Game
{
    sealed class FriendZoneUISys : SystemViewAbstract, IEcsRunSystem
    {
        public FriendZoneUISys(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ButtonC.SetActiveParent(false);

            if (GameModeC.IsGameMode(GameModes.WithFriendOff))
            {
                if (Es.FriendZoneE.IsActiveC.IsActive)
                {
                    TextC.SetActiveParent(true);

                    if (Es.WhoseMove.CurPlayerI == PlayerTypes.First)
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
