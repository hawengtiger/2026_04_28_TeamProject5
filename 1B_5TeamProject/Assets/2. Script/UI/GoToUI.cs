using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToUI : MonoBehaviour
{

    public void GoToScene(string scene)
    {
        int totalDamage = PlayerPrefs.GetInt("TotalDamage", -1);

        if (totalDamage >= 0)
        {
            PlayerPrefs.SetString("GameMode", "DPSGameScene");
        }
        else
        {
            PlayerPrefs.SetString("GameMode", "NormalGameScene");
        }

        scene = PlayerPrefs.GetString("GameMode", "");

        if (scene == "DPSGameScene")
        {
            SceneManager.LoadScene(scene);
        }
        else if (scene == "NormalGameScene")
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void Main(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}