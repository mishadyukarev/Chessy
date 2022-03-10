namespace Chessy.Game.System.Model
{
    sealed class ClearEffectsKingS : SystemAbstract, IEcsRunSystem
    {
        internal ClearEffectsKingS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                E.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }
        }
    }
}