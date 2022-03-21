namespace Chessy.Game.System.Model
{
    public struct ReadyS
    {
        public void Ready(in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            e.RpcPoolEs.ReadyToMaster();
        }
    }
}