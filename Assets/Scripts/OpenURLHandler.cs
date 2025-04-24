using System.Runtime.InteropServices;
using UnityEngine;

public class OpenURLHandler : MonoBehaviour
{
    [SerializeField] string url;

    private void OnMouseDown()
    {
#if !UNITY_EDITOR
                Application.ExternalEval("window.open('" + url + "','_self')");
#endif
    }

    [DllImport("__Internal")]
    private static extern void openUrl(string url);
}