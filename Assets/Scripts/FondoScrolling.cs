using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1f; // Velocidad de desplazamiento
    private Vector2 startPosition; // Posición inicial del fondo

    void Start()
    {
        startPosition = transform.position; // Guarda la posición inicial
    }

    void Update()
    {
        // Mueve el fondo en el eje X
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, transform.localScale.x);
        transform.position = startPosition + Vector2.left * newPosition;
    }
}