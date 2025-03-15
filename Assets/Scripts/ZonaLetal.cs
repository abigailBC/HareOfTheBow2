using UnityEngine;

public class ZonaLetal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ReduceVidas();

            if (GameManager.instance.vidas > -1)
            {
                // Si aún quedan vidas, reiniciar posición del jugador
                other.transform.position = new Vector3(0, 2, 0);
            }
            else
            {
                // Si las vidas se agotaron, cargar la pantalla de Game Over
                Debug.Log("No quedan vidas. Cargando GameOverScene desde ZonaLetal...");
                GameManager.instance.LoadGameOverScene();
            }
        }
    }
}