using ECS;

namespace Game.Game
{
    public sealed class ScoutHeroCooldownE : EntityAbstract
    {
        public AmountC CooldownC => Ent.Get<AmountC>();

        internal ScoutHeroCooldownE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}