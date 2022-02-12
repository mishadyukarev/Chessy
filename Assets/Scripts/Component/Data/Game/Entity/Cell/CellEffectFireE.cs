using ECS;

namespace Game.Game
{
    public sealed class CellEffectFireE : CellEntityAbstract
    {
        ref HaveFireC HaveFireCRef => ref Ent.Get<HaveFireC>();
        public HaveFireC HaveFireC => Ent.Get<HaveFireC>();

        internal CellEffectFireE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {

        }

        public void SetFire(in bool needFire) => HaveFireCRef.Have = needFire;
        public void Disable() => HaveFireCRef.Have = false;
        public void Enable() => HaveFireCRef.Have = true;

        public void TryFireHell()
        {
            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
            {
                HaveFireCRef.Have = true;
            }
        }
        public void TryFireAfterShift(in CellEs[] cellEs)
        {
            if (cellEs[Idx].UnitEs.UnitE.UnitTC.Is(UnitTypes.Hell))
            {
                if (cellEs[Idx].EnvironmentEs.AdultForest.HaveEnvironment)
                {
                    HaveFireCRef.Have = true;
                }
            }
        }

        public void SyncRpc(in bool needFire) => HaveFireCRef.Have = needFire;
    }
}