using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Common
{
    public class LikeGame : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(delegate { Application.OpenURL(SpawnInitComSys.IRL_GAME_IN_GOOGLE_PLAY); });
        }
    }
}