using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToUI : MonoBehaviour
{ 
    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
