using UnityEngine;

[CreateAssetMenu(fileName = "ObjectSOData", menuName = "Scriptable Objects/ObjectSOData")]
public class ObjectSOData : ScriptableObject
{
    [Header("데미지")]
    public int damage;

    [Header("목표 범위")]
    public float range;

    [Header("포인트 속도")]
    public float speed;
}
