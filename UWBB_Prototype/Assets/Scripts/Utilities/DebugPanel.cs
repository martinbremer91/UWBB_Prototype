using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace MBre.Utilities
{
    public class DebugPanel : MonoBehaviour
    {
    #if UNITY_EDITOR
        private static DebugPanel instance;
        
        [SerializeField] private DebugFlags activeDebugFlags;
        [SerializeField] private GameObject debugChannelPrefab;

        private static DebugChannel[] debugChannels;
        private static Hashtable channelIndices = new();
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Init();
            }
            else
                Destroy(gameObject);
        }

        private void Init()
        {
            int channelCount = Enum.GetNames(typeof(DebugFlags)).Length - 2;
            debugChannels = new DebugChannel[channelCount];
            channelIndices.Clear();

            for (var i = 0; i < channelCount; i++)
            {
                debugChannels[i].flags = (DebugFlags)Mathf.Pow(2, i);
                
                GameObject channelGameObject = Instantiate(debugChannelPrefab, transform);
                channelGameObject.name = 
                    Enum.GetName(typeof(DebugFlags), debugChannels[i].flags) ?? "generic debug channel";
                debugChannels[i].text = channelGameObject.GetComponent<TMP_Text>();

                channelIndices.Add(debugChannels[i].flags, i);
            }
        }

        private void Update()
        {
            SetDebugPanelText("input test", DebugFlags.Input, true);
            SetDebugPanelText("controller test", DebugFlags.CharacterController);
        }
#endif

        public static void SetDebugPanelText(string message, DebugFlags flag, bool printConsole = false)
        {
    #if UNITY_EDITOR
            if (instance == null)
                Debug.LogError("SetDebugPanelText: DebugPanel instance cannot be null");
            
            if (!IsFlagValid())
                return;

            bool channelActive = instance.activeDebugFlags.HasFlag(flag);
            message = channelActive ? 
                $"[{Enum.GetName(typeof(DebugFlags), flag)}]: " + message : 
                String.Empty;
            
            int channelIndex = (int)channelIndices[flag];
            debugChannels[channelIndex].text.text = message;
            
            if (channelActive && printConsole)
                Debug.Log(message);

            bool IsFlagValid()
            {
                if (!Enum.IsDefined(typeof(DebugFlags), flag))
                {
                    Debug.LogError("SetDebugPanelText: flag parameter must be defined " +
                                   "(exactly one flag must be set)");
                    return false;
                }

                return flag != 0;
            }
#endif
        }
    }

    public struct DebugChannel
    {
        public DebugFlags flags;
        public TMP_Text text;
    }

    [Flags]
    public enum DebugFlags
    {
        None = 0,
        Input = 1 << 0,
        CharacterController = 1 << 1,
        All = ~0
    }
}
