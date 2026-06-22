
using UnityEngine;
using DG.Tweening;

public class CameraShakeEffect : MonoBehaviour
{
    public static CameraShakeEffect Instence;

    [Header("펀치 스케일 예시")]
    public Camera targetCamera;     // UI 타겟

    public float time = 0.3f;
    public float strong =10f;
    public int count = 20;
    public float rand = 90f;

    private void Awake()
    {
        Instence = this;
    }

    public void PlayCameraShake()
    {
        if (targetCamera == null)
            return;
        targetCamera.DOKill();     // 이전 실행 중이던 Tween이 있으면 정리한다.
        targetCamera.DOShakePosition(time, strong, count, rand);     // 시간, 강도, 진동 횟수, 랜덤성
    }

   
}
