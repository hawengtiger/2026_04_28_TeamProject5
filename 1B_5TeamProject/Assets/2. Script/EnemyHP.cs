using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHP : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;

    private void Start()
    {
        currentHp = maxHp;

        if (BossHPBar.Instance != null)
        {
            BossHPBar.Instance.SetBoss(this);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        Debug.Log("Enemy 피격! 데미지: " + damage);
        Debug.Log("Enemy 현재 체력: " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
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
}