using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para cambiar escenas

public class LifeManager : MonoBehaviour
{
    public Image[] lifeIcons; // Array de imágenes de vidas en la UI
    public Sprite lifeNormal; // Imagen de zanahoria naranja (vida activa)
    public Sprite lifeLost;   // Imagen de zanahoria negra (vida perdida)

    private static int lives = 3; // Variable estática para mantener vidas entre escenas
    private static LifeManager instance;

    void Awake()
    {
        // Asegurar que solo exista un LifeManager en la escena
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el GameManager en todas las escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados en otras escenas
        }
    }

    void Start()
    {
        UpdateLivesUI(); // Actualiza las vidas al inicio
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Se ejecuta cuando se carga una nueva escena
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Al cambiar de escena, busca los iconos de vidas en la nueva escena
        FindLifeIcons();
        UpdateLivesUI();
    }

    private void FindLifeIcons()
    {
        lifeIcons = new Image[3];

        for (int i = 0; i < 3; i++)
        {
            GameObject icon = GameObject.Find("Vida" + (i + 1));
            if (icon != null)
                lifeIcons[i] = icon.GetComponent<Image>();
        }
    }

    // Método para perder una vida
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--; // Reducir vida
            UpdateLivesUI(); // Actualizar interfaz gráfica
        }

        if (lives == 0)
        {
            GameOver();
        }
    }

    // Método para actualizar la UI de las vidas
    private void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
            {
                if (i < lives)
                    lifeIcons[i].sprite = lifeNormal; // Zanahoria naranja (vida activa)
                else
                    lifeIcons[i].sprite = lifeLost; // Zanahoria negra (vida perdida)
            }
        }
    }

    // Método para finalizar el juego
    private void GameOver()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("GameOverScene"); // Cargar escena de Game Over
    }
}