using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.General.Components
{
    internal struct OwnerBotComponent
    {
        internal bool HaveBot;

        internal void StartFill()
        {
            HaveBot = false;
        }
    }
}
