using System.Runtime.InteropServices;
using UnityEngine;

public class OpenURLHandler : MonoBehaviour
{
    [SerializeField] string url;

    private void OnMouseDown()
    {
#if !UNITY_EDITOR
        openUrl(url);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openUrl(string url);
}