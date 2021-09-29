using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TestButton : MonoBehaviour
{
    [SerializeField]private VideoClip[] _videoClips;
    private int _cur;

    private void Start()
    {
        var videoPlayer = GetComponent<VideoPlayer>();

        transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-500f, 500f), Random.Range(-300f, 300f));


        GetComponent<Button>().onClick.AddListener(delegate
        {
            ++_cur;

            if(_cur < _videoClips.Length)
            {
                videoPlayer.clip = _videoClips[_cur];

                transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-500f, 500f), Random.Range(-300f, 300f));
            }
            else
            {
                transform.parent.gameObject.SetActive(false);
            }
        });
    }
}
