using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public Transform controladorDisparo;

    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool jugadorDetectado;

    public GameObject balaEnemigo;
    public float tiempoEntreDisparo;
    public float tiempoDesdeUltimoDisparo;
    public float tiempoEsperaDisparo;

    public Animator animatorEnemigo;

    // Update is called once per frame
    private void Update()
    {
        jugadorDetectado = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        if (jugadorDetectado)
        {
            if (Time.time > tiempoEntreDisparo + tiempoDesdeUltimoDisparo)
            {
                tiempoDesdeUltimoDisparo = Time.time;
                animatorEnemigo.SetTrigger("Disparar");
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
    }

    private void Disparar()
    {
        Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }
}
