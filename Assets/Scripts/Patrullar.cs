using UnityEngine;

public class Patrullar : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private Vector3 destino;

    void Start()
    {
        destino = puntoB.position;
    }

    void Update()
    {
        // Mover hacie el destino
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
        // Si llega al destino, cambia de dirección
        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            destino = (destino == puntoA.position) ? puntoB.position : puntoA.position;
            Flip(); //Voltear
        }
    }

    void Flip()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    void OnDrawGizmos()
    {
        // Dibuja una línea entre puntoA y puntoB en el editor
        if (puntoA != null && puntoB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(puntoA.position, puntoB.position);
        }
    }
}