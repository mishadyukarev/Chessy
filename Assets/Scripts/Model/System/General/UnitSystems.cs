using Chessy.Model.Entity;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Chessy.Model.System
{
    sealed class UnitSystems : SystemModelAbstract
    {
        internal readonly UnitAbilitiesSystems unitAbilitiesSs;

        internal UnitSystems(SystemsModel s, in EntitiesModel e) : base(s, e)
        {
            unitAbilitiesSs = new UnitAbilitiesSystems(s, e);
        }

        internal void AttackShield(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            _extraTWC[cell_0].ProtectionShield -= damage;
            if (!_extraTWC[cell_0].HaveAnyProtectionShield)
                _extraTWC[cell_0].Dispose();
        }

        internal void TryDestroyBuildingWithSimplePawnM(in byte cellIdx_0, in Player sender)
        {
            s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Destroy);

            BuildingC(cellIdx_0).Dispose();
        }
    }

    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal readonly ChangeCornerArcherS changeCornerArcherS;
        internal readonly StunElfemaleS_M stunElfemaleS_M;

        internal UnitAbilitiesSystems(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            changeCornerArcherS = new ChangeCornerArcherS(sMG, eMG);
            stunElfemaleS_M = new StunElfemaleS_M(sMG, eMG);
        }
    }
}