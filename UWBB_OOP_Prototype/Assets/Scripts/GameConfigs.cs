using UnityEngine;

[CreateAssetMenu(menuName = "UWBB/GameConfigs", fileName = "GameConfigs")]
public class GameConfigs : ScriptableObject
{
    public static GameConfigs instance;
    private void Awake() => instance = this;
}
