using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public float velocidadCamera = 10f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, velocidadCamera * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
