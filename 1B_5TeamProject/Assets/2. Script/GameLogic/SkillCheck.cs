using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    public static SkillCheck Instance;

    [Header("스킬체크 오브젝트")]
    public Transform point;
    public Transform target;

    [Header("데미지")]
    public int defaultDamage = 10;

    [Header("설정")]
    public bool hit = false;
    public float moveSpeed = 1f;

    public GameObject skillCheckPanel;

    private Rigidbody2D rb;
    private bool isMovingRight = true;

    private EnemyHP targetEnemy;
    private ObjectSOData attackData;
    private DraggableRank currentRank;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        skillCheckPanel.SetActive(false);
        rb = point.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!skillCheckPanel.activeSelf)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (isMovingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    public void OpenSkillCheck(EnemyHP enemy, ObjectSOData data, DraggableRank rank)
    {
        targetEnemy = enemy;
        attackData = data;
        currentRank = rank;

        hit = false;

        ResetPointPosition();

        target.localScale = new Vector3(attackData.range, transform.localScale.y, transform.localScale.z);

        // 포인트 속도
        moveSpeed = attackData.speed;

        RandomizeTargetPosition();

        skillCheckPanel.SetActive(true);
    }

    void TryAttack()
    {
        if (targetEnemy == null)
        {
            CloseSkillCheck();
            return;
        }

        int damage = defaultDamage;

        if (hit)
        {
            damage = attackData.damage;
            Debug.Log("스킬체크 성공!");
        }
        else
        {
            Debug.Log("스킬체크 실패!");
        }

        targetEnemy.TakeDamage(damage);

        if (currentRank != null)
        {
            Destroy(currentRank.gameObject);
        }

        CloseSkillCheck();
    }

    int GetDamageByRank(int level)
    {
        if (level == 1)
            return 20;
        else if (level == 2)
            return 40;
        else if (level == 3)
            return 60;
        else
            return 80;
    }

    void SetTargetSizeByRank(int level) //초록 지점 크기
    {
        if (level == 1)
            target.localScale = new Vector3(0.1f, 1f, 1f);
        else if (level == 2)
            target.localScale = new Vector3(0.07f, 1f, 1f);
        else if (level == 3)
            target.localScale = new Vector3(0.05f, 1f, 1f);
        else
            target.localScale = new Vector3(1f, 1f, 1f);
    }

    void ResetPointPosition()
    {
        point.localPosition = new Vector3(
            -0.5f,
            point.localPosition.y,
            point.localPosition.z
        );

        isMovingRight = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void RandomizeTargetPosition()
    {
        float randomX = Random.Range(-0.44f, 0.44f);

        target.localPosition = new Vector3(
            randomX,
            target.localPosition.y,
            target.localPosition.z
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMovingRight = !isMovingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Red"))
        {
            Debug.Log("지금!");
            hit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Red"))
        {
            Debug.Log("클릭 범위 나감");
            hit = false;
        }
    }

    public void CloseSkillCheck()
    {
        skillCheckPanel.SetActive(false);

        targetEnemy = null;
        currentRank = null;
        hit = false;
    }
}