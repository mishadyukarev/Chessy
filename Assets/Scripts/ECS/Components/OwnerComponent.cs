using Photon.Realtime;

internal struct OwnerComponent
{
    internal Player Owner { get; set; }

    internal void StartFill(Player owner = default) => Owner = owner;
}
