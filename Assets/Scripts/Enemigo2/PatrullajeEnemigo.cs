using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullajeEnemigo : MonoBehaviour
{
    public float radioBusqueda;

    public LayerMask capaJugador;

    public Transform transformJugador;

    public EstadosMovimiento estadoActual;

    public float velocidadMovimiento;

    public float distanciaMaxima;

    public Vector3 puntoInicial;

    public bool mirandoDerecha;

    public Rigidbody2D rb;

    public Animator animator;

    public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo,
    }

    private void Start()
    {
        puntoInicial = transform.position;
        
    }


    private void Update()
    {
        switch(estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.Siguiendo:
                // L�gica para seguir al jugador
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.Volviendo:
                // L�gica para volver a la posici�n inicial
                EstadoVolviendo();
                break;
        }

        
    }

    private void EstadoEsperando()
    {
        // L�gica para el estado de esperando
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);
        if (jugadorCollider)
        {
            transformJugador = jugadorCollider.transform;

            estadoActual = EstadosMovimiento.Siguiendo;
        }
    }

    private void EstadoSiguiendo()
    {
        // L�gica para el estado de siguiendo
        animator.SetBool("Corriendo", true);

        if (transformJugador == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }
        
        if(transform.position.x < transformJugador.position.x)
        {
            rb.linearVelocity = new Vector2(velocidadMovimiento, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidadMovimiento, rb.linearVelocity.y);
        }

        GirarAObjetivo(transformJugador.position);

        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima || 
            Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
        }
    }

    private void EstadoVolviendo()
    {
        // L�gica para el estado de volviendo
        if (transform.position.x < puntoInicial.x)
        {
            rb.linearVelocity = new Vector2(velocidadMovimiento, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidadMovimiento, rb.linearVelocity.y);
        }

        GirarAObjetivo(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;

            animator.SetBool("Corriendo", false);

            estadoActual = EstadosMovimiento.Esperando;
        }
    }

    private void GirarAObjetivo(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            Girar();
        }
        else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y +180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }

}
