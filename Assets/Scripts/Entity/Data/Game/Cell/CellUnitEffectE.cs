using ECS;

namespace Game.Game
{
    public abstract class CellUnitEffectE : CellEntityAbstract
    {
        readonly EffectTypes EffectT;

        protected CellUnitEffectE(in EffectTypes effectT, in CellEs cellEs, in EcsWorld gameW) : base(cellEs, gameW)
        {
            EffectT = effectT;
        }
    }
}