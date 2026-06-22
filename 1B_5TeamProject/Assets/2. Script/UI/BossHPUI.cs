using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public static BossHPBar Instance;

    [Header("보스 HP 바")]
    public Image hpFill;

    [Header("HP 바 전체")]
    public GameObject hpBar;

    private EnemyHP boss;

    private void Awake()
    {
        Instance = this;

        if (hpBar != null)
            hpBar.SetActive(false);
    }

    private void Update()
    {
        if (boss == null)
            return;

        hpFill.fillAmount = (float)boss.currentHp / boss.maxHp;
    }

    public void SetBoss(EnemyHP target)
    {
        boss = target;

        if (hpBar != null)
            hpBar.SetActive(true);

        hpFill.fillAmount = 1f;
    }

    public void ClearBoss()
    {
        boss = null;

        if (hpBar != null)
            hpBar.SetActive(false);
    }
}