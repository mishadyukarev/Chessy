namespace Game.Game
{
    public abstract class SystemViewAbstract : SystemAbstract
    {
        protected readonly EntitiesView VEs;

        public CellVEs CellVEs(in byte idx) => VEs.CellVEs(idx);

        public CellUnitVEs UnitVEs(in byte idx) => CellVEs(idx).UnitVEs;
        public CellUnitEffectVEs UnitEffectVEs(in byte idx) => UnitVEs(idx).EffectVEs;

        public SystemViewAbstract(in Entities ents, in EntitiesView entsView) : base(ents)
        {
            VEs = entsView;
        }
    }
}