using Game.Common;
using static Game.Game.CenterFriendUIE;

namespace Game.Game
{
    struct FriendZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            ButtonC.SetActiveParent(false);

            if (GameModeC.IsGameMode(GameModes.WithFriendOff))
            {
                if (EntityPool.FriendZone<IsActiveC>().IsActive)
                {
                    TextC.SetActiveParent(true);

                    if (WhoseMoveE.CurPlayerI == PlayerTypes.First)
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
