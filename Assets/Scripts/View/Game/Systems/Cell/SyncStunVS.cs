using Chessy.Game.Entity;

namespace Chessy.Game
{
    sealed class SyncStunVS
    {
        bool _needActive;

        public void Sync(in byte idx_0, in EntitiesViewGame eV, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            _needActive = false;

            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitVisibleC(idx_0).IsVisible(e.CurPlayerITC.PlayerT))
                {
                    _needActive = e.StunUnitC(idx_0).IsStunned;
                }
            }

            eV.UnitEffectVEs(idx_0).StunSRC.GameObject.SetActive(_needActive);
        }
    }
}