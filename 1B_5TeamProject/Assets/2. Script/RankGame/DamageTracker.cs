using TMPro;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{
    public static DamageTracker Instance;

    public int totalDamage = 0;

    public TextMeshProUGUI damageText;

    private void Awake()
    {
        if (Instance == null)    //Instance가 없으면 생성함.
        {
            Instance = this;        //싱글톤 등록
            DontDestroyOnLoad(gameObject);                              // 씬 전환에도 제거 안되게 함.
        }
        else
        {
            Destroy(gameObject);    //(이미 있다면 추가 안함.) SoundManager가 2개 생기는 거 방지
        }
        damageText.text = "누적 데미지 : " + totalDamage.ToString();
        PlayerPrefs.SetInt("TotalDamage", totalDamage);
        PlayerPrefs.Save();
    }

    public void AddDamage(int damage)
    {
        totalDamage += damage;

        if (damageText != null)
        {
            damageText.text = "누적 데미지 : " + totalDamage.ToString();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("TotalDamage", totalDamage);
        PlayerPrefs.Save();
    }

    public void ResetDamage()
    {
        totalDamage = 0;
    }

    public void DeleteSaveFile()
    {
        PlayerPrefs.DeleteKey("TotalDamage");
    }
}