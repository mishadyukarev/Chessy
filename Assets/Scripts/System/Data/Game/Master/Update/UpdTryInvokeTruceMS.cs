using Photon.Pun;

namespace Game.Game
{
    public sealed class UpdTryInvokeTruceMS : SystemAbstract, IEcsRunSystem
    {
        readonly SystemsMaster _systemsMaster;

        internal UpdTryInvokeTruceMS(in SystemsMaster systemsMaster, in Entities ents) : base(ents)
        {
            _systemsMaster = systemsMaster;
        }

        public void Run()
        {
            var amountAdultForest = 0;

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    amountAdultForest++;
            }

            if (amountAdultForest <= UpdateValues.NEED_ADULT_FORESTS_FOR_TRUCE)
            {
                Es.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                _systemsMaster.InvokeRun(SystemDataMasterTypes.Truce);
            }
        }
    }
}