using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneWeaver : MonoBehaviour
{
    public void ToMainScreen()
    {
        SceneManager.LoadScene(1);
    }
}
