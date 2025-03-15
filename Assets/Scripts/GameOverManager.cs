using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
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
            GameManager.instance.UpdateLivesUI(); // Actualizar la UI para que muestre todas las vidas activas
        }

        SceneManager.LoadScene("SampleScene"); // Cambia esto por el nombre real de tu escena principal
    }
}