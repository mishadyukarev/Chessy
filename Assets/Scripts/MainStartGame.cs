using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Chessy
{
    sealed class MainStartGame : MonoBehaviour
    {
        [SerializeField] Image Image;

        void Start()
        {
            //SceneManager.LoadScene()

            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            var operation = SceneManager.LoadSceneAsync("Game");
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                //Debug.Log(operation.progress);

                Image.fillAmount = operation.progress;

                if (operation.progress <= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}


