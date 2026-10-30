using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Editor References")]
    public Rigidbody playerRb;
    public AudioSource playerAudio;

    [Header("Sound Configuration")]

    public AudioClip[] soundCollection;

    [Header("Movement Parameters")]

    public float speed = 10;
    public Vector2 movementInput; // Entrada y almacén del movimiento del jugador

    [Header("Jump Parameters")]

    public float jumpForce = 6;
    public bool isGrounded = true; // indica si el jugador está en el suelo
    public int remainingJumps = 2;


    
    [Header("Respawn System")]
    public float fallLimit = -5f; // límite de caída para reiniciar
    public Transform respawnPoint; // punto de reaparición del jugador


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //CinematicMovement();
        if (transform.position.y <= fallLimit) // si la posición del jugador en el eje Y es menor que el límite de caída
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {
        PhysicalMovement();
    }

    void CinematicMovement()
    {
        //Movimiento = (Dirección * Velocidad * Input * Time.DeltaTime)

        //transform.Translate(Vector3.right * speed * movementInput.x * Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * movementInput.y * Time.deltaTime);
    }

    void PhysicalMovement()
    {
        playerRb.AddForce(Vector3.right * speed * movementInput.x);
        playerRb.AddForce(Vector3.forward * speed * movementInput.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // si el jugador colisiona con un objeto con el tag ground
        {
            isGrounded = true; // el jugador está en el suelo
            remainingJumps = 2;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Respawn();
        }

    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        PlaySFX(0);
    }

    void Respawn()
    {
        transform.position = respawnPoint.position; // reaparecer
        playerRb.linearVelocity = Vector3.zero; // resetear la velocidad del Rb
        PlaySFX(1);
    }

    public void PlaySFX(int soundToplay)
    {
        playerAudio.PlayOneShot(soundCollection[soundToplay]);
    }



    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>(); // Almacena la entrada de movimiento del jugador en un Vector2
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && remainingJumps > 0)
        {
            isGrounded = false; // el jugador ya no está en el suelo
            remainingJumps --;
            Jump(); // llamada al método de salto
        }

    }
}
    #endregion