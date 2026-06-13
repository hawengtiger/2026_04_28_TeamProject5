using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// === | 설정UI창 갱신 | ===
/// </summary>
public class ScreenUI : MonoBehaviour
{
    public GameObject settingPanel;
    
    public Button start,setting,quit;

    public string scene;

    private void Start()
    {
        Reset();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// === | 게임 설정 | ===
    /// </summary>
    // 매개변수 이름을 'ture'에서 'isActive' 또는 'true'를 뜻하는 올바른 명칭으로 수정합니다.
    public void Setting(bool isActive)
    {
        settingPanel.SetActive(isActive);
    }

    /// <summary>
    /// === | 게임 종료 | ===
    /// </summary>
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 종료
#else
        Application.Quit(); // 빌드된 게임에서 종료
#endif
    }

    /// <summary>
    /// === | 초기 세팅 | ===
    /// </summary>
    private void Reset()
    {
        settingPanel.SetActive(false);
        start.onClick.AddListener(StartGame);
        quit.onClick.AddListener(GameQuit);
        setting.onClick.AddListener(() => Setting(true));
    }
}
