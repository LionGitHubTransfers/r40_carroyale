using System.IO;
using UnityEditor;
using UnityEngine;

public class ScreenshotUtils : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Utils/Screenshot")]
    public static void MakeScrenshot()
    {
        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Screenshots");

        string path = "Screenshots/Screenshot" +
            "_" + Camera.main.pixelWidth + "x" + Camera.main.pixelHeight +
            "_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";

        ScreenCapture.CaptureScreenshot(path);
        Debug.Log(path);
    }
#endif

}

