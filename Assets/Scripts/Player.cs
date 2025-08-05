using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    public float speed = 5f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!IsOwner) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, y, 0) * speed * Time.deltaTime);
    }
}
