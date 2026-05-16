using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    public Transform puntoSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private bool estaEnSuelo;
    private SpriteRenderer sr;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    void Update()
    {
        estaEnSuelo = Physics2D.OverlapCircle(puntoSuelo.position, radioSuelo, capaSuelo);

        float h = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) h = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) h = 1f;

        if (h != 0f)
            sr.flipX = h < 0;

        rb.linearVelocity = new Vector2(h * velocidad, rb.linearVelocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && estaEnSuelo)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);

        WrapPantalla();
    }

    void WrapPantalla()
    {
        float mitadAncho = cam.orthographicSize * cam.aspect;
        Vector3 pos = transform.position;

        if (pos.x > mitadAncho)
            pos.x = -mitadAncho;
        else if (pos.x < -mitadAncho)
            pos.x = mitadAncho;

        transform.position = pos;
    }
}
