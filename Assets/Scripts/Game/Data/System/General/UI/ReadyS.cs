namespace Chessy.Game.System.Model
{
    public struct ReadyS
    {
        public void Ready(in EntitiesModel e)
        {
            e.RpcPoolEs.ReadyToMaster();
        }
    }
}