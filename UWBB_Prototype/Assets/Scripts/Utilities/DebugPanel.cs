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
            int channelCount = Enum.GetNames(typeof(DebugFlags)).Length - 2; // -2 excludes 'None' and 'All'
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
#endif

        public static void CustomDebug(string message, DebugFlags flag = DebugFlags.Default, bool printConsole = false)
        {
    #if UNITY_EDITOR
            int channelIndex = (int)channelIndices[flag];
            bool flagActive = instance.activeDebugFlags.HasFlag(flag);

            if (!flagActive && debugChannels[channelIndex].active) 
                debugChannels[channelIndex].text.text = String.Empty;

            debugChannels[channelIndex].active = flagActive;
            
            if (!debugChannels[channelIndex].active || !IsFlagValid(flag))
                return;
            
            SetDebugText(message, debugChannels[channelIndex], flag, printConsole);
#endif
        }
        
#if UNITY_EDITOR
        private static bool IsFlagValid(DebugFlags flag)
        {
            if (!Enum.IsDefined(typeof(DebugFlags), flag))
            {
                Debug.LogError("SetDebugPanelText: flag parameter must be defined " +
                               "(exactly one flag must be set)");
                return false;
            }

            return flag != 0;
        }

        private static void SetDebugText(string message, DebugChannel channel, DebugFlags flag, bool print)
        {
            message = $"[{Enum.GetName(typeof(DebugFlags), flag)}]: " + message;
            channel.text.text = message;
            
            if (print) Debug.Log(message);
        }
#endif
    }

    public struct DebugChannel
    {
        public DebugFlags flags;
        public TMP_Text text;
        public bool active;
    }

    [Flags]
    public enum DebugFlags
    {
        None = 0,
        Default = 1 << 0,
        Input = 1 << 1,
        CharacterController = 1 << 2,
        All = ~0
    }
}
