namespace Game.Game
{
    public abstract class SystemViewAbstract : SystemAbstract
    {
        protected readonly EntitiesView VEs;

        protected CellVEs CellVEs(in byte idx) => VEs.CellEs(idx);

        protected CellUnitVEs UnitVEs(in byte idx) => CellVEs(idx).UnitVEs;
        protected CellUnitEffectVEs UnitEffectVEs(in byte idx) => UnitVEs(idx).EffectVEs;

        public SystemViewAbstract(in EntitiesModel ents, in EntitiesView entsView) : base(ents)
        {
            VEs = entsView;
        }
    }
}