using ECS;

namespace Game.Game
{
    public sealed class PeopleInCityE : EntityAbstract
    {
        readonly PlayerTypes _player;
        ref AmountC AmountCRef => ref Ent.Get<AmountC>();
        AmountC AmountC => Ent.Get<AmountC>();

        public int People
        {
            get => AmountC.Amount;
            set => AmountCRef.Amount = value;
        }

        public bool CanGetPeople => AmountC.HaveAny;

        internal PeopleInCityE(in PlayerTypes player, in EcsWorld gameW) : base(gameW)
        {
            _player = player;
            People = MaxPeopleInCityValues.MAX_PEOPLE_IN_CITY_START_GAME;
        }
    }
}