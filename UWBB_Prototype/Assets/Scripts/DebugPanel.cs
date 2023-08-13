using System;
using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
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

    public static void SetDebugPanelText(string message)
    {
        if (instance == null)
            return;

        instance.text.text = message;
    }
}
