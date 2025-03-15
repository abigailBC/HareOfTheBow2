using UnityEngine;
using UnityEngine.SceneManagement;

public class HasGanado : MonoBehaviour
{
    void Start()
    {
        if (Camera.main != null)
            Camera.main.clearFlags = CameraClearFlags.Depth;
    }

    public void RestartGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.vidas = 3; // Restablecer vidas a 3
            GameManager.instance.UpdateLivesUI();
        }

        SceneManager.LoadScene("SampleScene");
    }
}