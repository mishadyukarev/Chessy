using Chessy.Common;
using Chessy.Game.Entity.Model;
using Photon.Pun;

namespace Chessy.Game
{
    public static class TryInvokeTruceUpdateMS
    {
        public static void Truce(in GameModeTC gameModeTC, EntitiesModelGame e)
        {
            var amountAdultForest = 0;

            for (byte cell_0 = 0; cell_0 < e.LengthCells; cell_0++)
            {
                if (e.AdultForestC(cell_0).HaveAnyResources)
                    amountAdultForest++;
            }

            if (amountAdultForest <= UPDATE_VALUES.NEED_ADULT_FORESTS_FOR_TRUCE)
            {
                e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);


                new TruceMS().Run(gameModeTC, e);
            }
        }
    }
}