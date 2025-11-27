using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    public int cantidadVida;

    public void TomarDaño(int daño)
    {
        cantidadVida -= daño;
        if (cantidadVida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
