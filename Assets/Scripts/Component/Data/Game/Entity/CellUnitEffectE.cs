using ECS;

namespace Game.Game
{
    public abstract class CellUnitEffectE : CellEntityAbstract
    {
        readonly EffectTypes EffectT;

        protected CellUnitEffectE(in EffectTypes effectT, in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
            EffectT = effectT;
        }
    }
}