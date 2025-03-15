using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton para acceso global
    public int vidas = 3; // Número de vidas compartidas entre escenas

    private static GameManager audioListenerInstance;

    public Image[] lifeIcons; // Array de imágenes de vidas en la UI
    public Sprite lifeNormal; // Imagen de zanahoria naranja (vida activa)
    public Sprite lifeLost;   // Imagen de zanahoria negra (vida perdida)

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioListenerInstance == null)
        {
            audioListenerInstance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        UpdateLivesUI();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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

    public void ReduceVidas()
    {
        Debug.Log("Vidas antes de reducir: " + vidas);

        vidas--; // Reducir una vida
        UpdateLivesUI(); // Actualizar la UI de vidas

        if (vidas <= -1)
        {
            Debug.Log("Vidas agotadas. Cargando GameOverScene...");
            LoadGameOverScene();
            return;
        }

        Debug.Log("Vidas restantes: " + vidas + ". Reiniciando la escena...");
        ReloadScene(); // Reiniciar la escena solo si quedan vidas.
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
            {
                lifeIcons[i].sprite = (i < vidas) ? lifeNormal : lifeLost;
            }
        }
    }
}