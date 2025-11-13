using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private float move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        transform.Translate(move*speed * Time.deltaTime, 0, 0);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        
    }
}
