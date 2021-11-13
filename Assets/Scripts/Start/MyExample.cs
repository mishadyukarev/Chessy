using UnityEngine;
using System.IO;
using UnityEngine.Video;

// Application-streamingAssetsPath example.
//
// Play a video and let the user stop/start it.
// The video location is StreamingAssets. The video is
// played on the camera background.

public class MyExample : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private string status;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Obtain the location of the video clip.
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "BonusKing.mp4");

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        // Do not show the video until the user needs it.
        videoPlayer.Play();

        status = "Press to play";
    }

    //void OnGUI()
    //{
    //    GUIStyle buttonWidth = new GUIStyle(GUI.skin.GetStyle("button"));
    //    buttonWidth.fontSize = 18 * (Screen.width / 800);

    //    if (GUI.Button(new Rect(Screen.width / 16, Screen.height / 16, Screen.width / 3, Screen.height / 8), status, buttonWidth))
    //    {
    //        if (videoPlayer.isPlaying)
    //        {
    //            videoPlayer.Pause();
    //            status = "Press to play";
    //        }
    //        else
    //        {
    //            videoPlayer.Play();
    //            status = "Press to pause";
    //        }
    //    }
    //}
}