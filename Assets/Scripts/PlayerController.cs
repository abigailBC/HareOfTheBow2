using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 30f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true; // Asumo que empieza mirando a la derecha

    public Sprite idleSprite;  // Sprite cuando está quieto
    public Sprite movementSprite;  // Sprite cuando se mueve
    private SpriteRenderer spriteRenderer;  // Componente para cambiar los sprites

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Obtener el SpriteRenderer
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Cambiar el sprite dependiendo del movimiento
        if (Mathf.Abs(move) > 0.1f)
        {
            spriteRenderer.sprite = movementSprite;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Game Over si cae demasiado
        if (transform.position.y < -10f)
        {
            GameManager.instance.ReduceVidas();

            if (GameManager.instance.vidas <= -1)
            {
                SceneManager.LoadScene("GameOverScene");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (collision.gameObject.CompareTag("EnemyFire"))
        {
            GameManager.instance.ReduceVidas();

            if (GameManager.instance.vidas <= -1)
            {
                SceneManager.LoadScene("GameOverScene");
            }
            else
            {
                Die();
            }
        }

        if (collision.gameObject.CompareTag("Goal2"))
            Win();

        if (collision.gameObject.CompareTag("Goal1"))
        {
            SceneManager.LoadScene("Scene2");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void Win()
    {
        SceneManager.LoadScene("JuegoSuperadoScene");
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}