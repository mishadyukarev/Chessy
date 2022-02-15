using ECS;

namespace Game.Game
{
    public sealed class PeopleInCityE : EntityAbstract
    {
        readonly PlayerTypes _player;
        public ref AmountC AmountC => ref Ent.Get<AmountC>();

        internal PeopleInCityE(in PlayerTypes player, in EcsWorld gameW) : base(gameW)
        {
            _player = player;
            AmountC.Amount = StartValues.PEOPLE_IN_CITY_IN_START_GAME;
        }
    }
}