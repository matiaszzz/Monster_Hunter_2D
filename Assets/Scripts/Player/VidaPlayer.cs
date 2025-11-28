using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    [SerializeField] private float cantidadVida;

    public void TomarDanio(float danio)
    {
        cantidadVida -= danio;
        if (cantidadVida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
