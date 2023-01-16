#if UNITY_EDITOR

using UnityEngine;

public class ScreenshotUtility : MonoBehaviour
{
    private const string folderPath = "Assets/Screenshots/";

    private void Start()
    {
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // capture screenshot on pressing C
        {
            int resolutionScale = 1; // set 1 for 1080p, 2 for 4K

            var screenshotName = "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
            ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), resolutionScale);
            Debug.Log(folderPath + screenshotName);
        }
    }
}

#endif
