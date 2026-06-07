using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;

    private void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        Debug.Log("Enemy 피격! 데미지: " + damage);
        Debug.Log("Enemy 현재 체력: " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy 사망");
        Destroy(gameObject);
    }
}