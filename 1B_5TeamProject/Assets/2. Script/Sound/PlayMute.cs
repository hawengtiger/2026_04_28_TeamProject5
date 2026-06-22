using UnityEngine;

/// <summary>
/// === | 브금 시작. | ===
/// </summary>
public class PlayMute : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
                SoundManager.Instance.StopMusic();
    }
}
