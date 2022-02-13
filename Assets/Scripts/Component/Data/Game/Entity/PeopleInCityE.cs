using ECS;

namespace Game.Game
{
    public sealed class PeopleInCityE : EntityAbstract
    {
        readonly PlayerTypes _player;
        ref AmountC AmountCRef => ref Ent.Get<AmountC>();

        public int People
        {
            get => AmountCRef.Amount;
            set => AmountCRef.Amount = value;
        }

        public bool CanGetPeople => AmountCRef.HaveAny;

        internal PeopleInCityE(in PlayerTypes player, in EcsWorld gameW) : base(gameW)
        {
            _player = player;
            People = MaxPeopleInCityValues.PEOPLE_IN_CITY_IN_START_GAME;
        }
    }
}