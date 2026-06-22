using TMPro;
using UnityEngine;

public class ResultDamageUI : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    private void Start()
    {
        if (damageText == null)
            return;

        int totalDamage = PlayerPrefs.GetInt("TotalDamage", -1);

        if (totalDamage < 0)
            return;

        damageText.text = $"누적 데미지 : {totalDamage}";
    }
}