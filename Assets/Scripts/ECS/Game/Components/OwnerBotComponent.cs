using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.General.Components
{
    internal struct OwnerBotComponent
    {
        private bool _haveBot;

        internal bool HaveBot => _haveBot;

        internal void SetBot(bool haveBot) => _haveBot = haveBot;

        internal void StartFill() => _haveBot = default;
    }
}
