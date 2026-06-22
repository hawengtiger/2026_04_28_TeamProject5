using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;
    public TextMeshProUGUI hpTxT;

    private void Start()
    {
        currentHp = maxHp;

        if (BossHPBar.Instance != null)
        {
            BossHPBar.Instance.SetBoss(this);
        }

        Text();
    }

    public void TakeDamage(int damage)
    {
        CameraShakeEffect.Instence.PlayCameraShake();

        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (DamageTracker.Instance != null)
        {
            DamageTracker.Instance.AddDamage(damage);
            DamageTracker.Instance.Save();
        }

        Debug.Log("Enemy 피격! 데미지: " + damage);
        Debug.Log("Enemy 현재 체력: " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }

        Text();
    }

    void Die()
    {
        if (DamageTracker.Instance != null)
        {
            DamageTracker.Instance.Save();
        }

        if (BossHPBar.Instance != null)
        {
            BossHPBar.Instance.ClearBoss();

            if (currentHp <= 0)
            {
                SceneManager.LoadScene("ClearScene");
            }
        }

        Debug.Log("Enemy 사망");
        Destroy(gameObject);
    }

    public void Text()
    {
        if (hpTxT == null)
            return;
        
        hpTxT.text = currentHp.ToString() + " / " + maxHp.ToString();
    }
}