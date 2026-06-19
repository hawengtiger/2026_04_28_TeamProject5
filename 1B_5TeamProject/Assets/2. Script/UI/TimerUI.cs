using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [Header("Å¸ÀÌ¸Ó")]
    public float maxTime = 60f;
    public float currentTime;

    [Header("UI")]
    public Image timerProgress;
    public TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = maxTime;

        UpdateTimerUI();
    }

    void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;

            currentTime = Mathf.Clamp(currentTime, 0f, maxTime);

            UpdateTimerUI();
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    void UpdateTimerUI()
    {
        float progress = currentTime / maxTime;

        if (timerProgress != null)
            timerProgress.fillAmount = progress;

        if (timerText != null)
        {
            int time = Mathf.CeilToInt(currentTime);
            timerText.text = time.ToString() + "'s";
        }

        if (currentTime <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}