using Photon.Pun;
using System;

namespace Chessy.Game
{
    public sealed class TryInvokeTruceUpdateMS : SystemAbstract, IEcsRunSystem
    {
        readonly Action _truce;

        internal TryInvokeTruceUpdateMS(in Action truce, in EntitiesModel ents) : base(ents)
        {
            _truce = truce;
        }

        public void Run()
        {
            var amountAdultForest = 0;

            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.AdultForestC(idx_0).HaveAnyResources)
                    amountAdultForest++;
            }

            if (amountAdultForest <= Update_VALUES.NEED_ADULT_FORESTS_FOR_TRUCE)
            {
                E.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                _truce.Invoke();
            }
        }
    }
}