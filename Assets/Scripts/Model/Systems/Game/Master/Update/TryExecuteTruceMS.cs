using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class TryExecuteTruceMS : SystemModel
    {
        readonly TruceS_M _truceS;

        internal TryExecuteTruceMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {

        }

        internal void TryExecute()
        {
            byte cellIdx0;

            var amountAdultForest = 0;

            for (cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.AdultForestC(cellIdx0).HaveAnyResources)
                    amountAdultForest++;
            }

            var can = !eMG.PlayerInfoE(PlayerTypes.First).PawnInfoC.HaveAnyPeopleInCity
                && !eMG.PlayerInfoE(PlayerTypes.Second).PawnInfoC.HaveAnyPeopleInCity;



            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE || can)
            {
                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);

                _truceS.Truce();
            }
        }
    }
}