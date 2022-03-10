namespace Chessy.Game.System.Model
{
    sealed class ClearEffectsKingS : SystemAbstract, IEcsRunSystem
    {
        readonly PlayerTypes _playerT;

        internal ClearEffectsKingS(in PlayerTypes playerT, in EntitiesModel ents) : base(ents)
        {
            _playerT = playerT;
        }

        public void Run()
        {
            E.PlayerInfoE(_playerT).WhereKingEffects.Clear();
        }
    }
}