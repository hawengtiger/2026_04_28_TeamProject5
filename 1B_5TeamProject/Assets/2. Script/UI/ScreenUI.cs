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
    public GameObject modPanel;

    public Button start,normal_start, dps_start, setting,quit;

    public string[] scene;

    private void Start()
    {
        Reset();
    }

    public void StartGame(bool isActive)
    {
        modPanel.SetActive(isActive); 
    }


    public void NormalGame()
    {
        SceneManager.LoadScene(scene[0]);
    }

    public void DPSGame()
    {
        SceneManager.LoadScene(scene[1]);
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
        modPanel.SetActive(false);

        start.onClick.AddListener(() => StartGame(true));
        normal_start.onClick.AddListener(NormalGame);
        dps_start.onClick.AddListener(DPSGame);
        quit.onClick.AddListener(GameQuit);
        setting.onClick.AddListener(() => Setting(true));

        if (PlayerPrefs.HasKey("TotalDamage"))
        {
            DamageTracker.Instance.DeleteSaveFile();
        }
        else
        {
            return;
        }
    }
}
