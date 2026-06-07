using UnityEngine;

public class DraggableRank : MonoBehaviour
{
    [Header("설정")]
    public int rankLevel = 1;

    [Header("공격 판정")]
    public float attackCheckRadius = 0.5f;

    public float dragSpeed = 30f;

    [Header("공격")]
    public bool attack = false;

    [Header("머지")]
    public float mergeDistance = 1f;

    [Header("영역")]
    public BoxCollider2D playArea;

    public bool isDragging = false;

    public ObjectSOData data;

    private Vector3 dragOffset;

    // 마지막으로 박스 안에 있었던 위치
    private Vector3 lastValidPosition;

    private Camera mainCamera;

    public SpriteRenderer spriteRenderer;

    public RankGameManager gameManager;

    void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindAnyObjectByType<RankGameManager>();

        // 게임매니저의 박스 영역 가져오기
        playArea = gameManager.spawnArea;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 targetPosition =
                GetMouseWorldPosition() + dragOffset;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                dragSpeed * Time.deltaTime
            );

            // 박스 안일 때만 저장
            if (IsInsideBox(transform.position))
            {
                lastValidPosition = transform.position;
            }
        }
    }

    void OnMouseDown()
    {
        StartDragging();
        Debug.Log(transform.position);
        Debug.Log("눌");
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        StopDragging();

        Debug.Log("뗌");
    }

    void StartDragging()
    {
        isDragging = true;

        dragOffset = transform.position - GetMouseWorldPosition();

        // 드래그 시작 위치 저장
        lastValidPosition = transform.position;

        spriteRenderer.sortingOrder = 999;
    }

    void StopDragging()
    {
        isDragging = false;
        spriteRenderer.sortingOrder = 10;

        // 마우스 뗀 위치에 Enemy가 있는지 먼저 검사
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, attackCheckRadius, LayerMask.GetMask("Default"));

        if (enemy != null && enemy.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = enemy.GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                AttackEnemy(enemyHP);
            }

            transform.position = lastValidPosition;
            return;
        }

        // Enemy가 아니면 그냥 복귀
        transform.position = lastValidPosition;

        CheckMerge();
    }

    void AttackEnemy(EnemyHP enemy)
    {
        SkillCheck.Instance.OpenSkillCheck(
            enemy,
            data,
            this);
    }

    bool IsInsideBox(Vector3 pos)
    {
        return playArea.bounds.Contains(pos);
    }

    void CheckMerge()
    {
        foreach (DraggableRank other in gameManager.ranks)
        {
            if (other == this)
                continue;

            float distance = Vector2.Distance(transform.position, other.transform.position);

            // 가까운가?
            if (distance <= mergeDistance)
            {
                // 같은 레벨인가?
                if (other.rankLevel == rankLevel)
                {
                    gameManager.MergeRanks(this, other);

                    return;
                }
            }
        }

        // 실패 시 마지막 안전 위치로 복귀
        transform.position = lastValidPosition;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z =
            -mainCamera.transform.position.z;

        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    public void SetRankLevel(int level)
    {
        rankLevel = level;

        if (gameManager != null)
        {
            spriteRenderer.sprite =
                gameManager.rankSprites[level - 1];

            data =
                gameManager.rankDatas[level - 1];
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.position = lastValidPosition;
    }

    private void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.ranks.Remove(this);
        }
    }
}