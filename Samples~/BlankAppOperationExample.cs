using UnityEngine;

public class BlankAppOperationExample : MonoBehaviour
{
    private string build_id = string.Empty;
#if UNITY_ANDROID
    void OnGUI()
    {
        build_id = GUILayout.TextField(build_id, GUILayout.Width(520), GUILayout.Height(200));

        if (GUILayout.Button("IsExist", GUILayout.Width(200), GUILayout.Height(100)))
        {
            Debug.Log(BlankAppOperation.IsExist(build_id));
        }

        if (GUILayout.Button("UninstallAPK", GUILayout.Width(200), GUILayout.Height(100)))
        {
            Debug.Log(BlankAppOperation.UninstallApp(build_id));
        }

        if (GUILayout.Button("OpenApp", GUILayout.Width(200), GUILayout.Height(100)))
        {
            Debug.Log(BlankAppOperation.OpenApp(build_id, build_id, build_id, build_id));
        }

        if (GUILayout.Button("InstallAPP", GUILayout.Width(200), GUILayout.Height(100)))
        {
            BlankAppOperation.InstallApp(Application.persistentDataPath + "/demo.apk");
        }
    }
#endif
}