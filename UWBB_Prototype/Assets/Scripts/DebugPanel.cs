using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
#if UNITY_EDITOR
    private static DebugPanel instance;

    private TMP_Text text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            text = GetComponent<TMP_Text>();
        }
        else
            Destroy(gameObject);
    }
#endif

    public static void SetDebugPanelText(string message)
    {
#if UNITY_EDITOR
        if (instance == null)
            return;

        instance.text.text = message;
#endif
    }
}
