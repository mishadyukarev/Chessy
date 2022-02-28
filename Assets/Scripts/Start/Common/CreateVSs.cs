using UnityEngine;

namespace Chessy.Common
{
    public sealed class CreateVSs
    {
        public CreateVSs(GameObject main)
        {
            main.AddComponent<PhotonSceneSys>();
        }
    }
}
