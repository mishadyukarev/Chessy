namespace Game.Game
{
    public readonly struct InstanceIDC : ICell
    {
        public readonly int InstanceID;

        public InstanceIDC(in int instanceID) => InstanceID = instanceID;
    }
}