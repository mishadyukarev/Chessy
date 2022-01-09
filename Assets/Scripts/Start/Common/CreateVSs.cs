using UnityEngine;

namespace Game.Common
{
    public sealed class CreateVSs
    {
        public CreateVSs(GameObject main)
        {
            main.AddComponent<PhotonSceneSys>();
        }
    }
}
