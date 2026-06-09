using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RankGameManager : MonoBehaviour
{
    [Header("생성 버튼")]
    public Button makeBT;

    [Header("개수 텍스트")]
    public TextMeshProUGUI objectText;

    [Header("생성 영역")]
    public BoxCollider2D spawnArea;

    [Header("계급장")]
    public GameObject rankPrefab;
    public Sprite[] rankSprites;
    public ObjectSOData[] rankDatas;

    [Header("설정")]
    public int maxRankLevel = 7;

    public int maxRankCount = 20; // 최대 생성 개수

    public List<DraggableRank> ranks = new List<DraggableRank>();

    void Start()
    {
        makeBT.onClick.AddListener(MakeRank);
    }

    void Update()
    {
        objectText.text = ranks.Count.ToString()+ " / 10";
    }

    public void MakeRank()
    {
        if (ranks.Count < maxRankCount)
        {
            SpawnNewRank();
        }
    }

    public void SpawnNewRank()
    {
        if (ranks.Count >= maxRankCount)
            return;

        Vector2 spawnPos = GetRandomPosition();

        GameObject obj = Instantiate(rankPrefab, spawnPos, Quaternion.identity);

        DraggableRank rank = obj.AddComponent<DraggableRank>();

        rank.SetRankLevel(1);

        ranks.Add(rank);
    }

    Vector2 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;

        float halfWidth = rankPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;

        float halfHeight = rankPrefab.GetComponent<SpriteRenderer>().bounds.extents.y;

        float x = Random.Range(bounds.min.x + halfWidth, bounds.max.x - halfWidth);

        float y = Random.Range(bounds.min.y + halfHeight, bounds.max.y - halfHeight);

        return new Vector2(x, y);
    }

    public void MergeRanks(DraggableRank from, DraggableRank target)
    {
        if (from == null || target == null)
            return;

        if (from.rankLevel != target.rankLevel)
        {
            from.ReturnToOriginalPosition();
            return;
        }

        int newLevel = target.rankLevel + 1;

        if (newLevel > maxRankLevel)
        {
            Destroy(from.gameObject);
            ranks.Remove(from);
            return;
        }

        target.SetRankLevel(newLevel);

        ranks.Remove(from);

        Destroy(from.gameObject);
    }
}