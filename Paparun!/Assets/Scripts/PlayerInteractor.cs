using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Points System")]
    public int points; // puntos del jugador actuales
    public int winPoints = 1; // puntos necesarios para ganar

    [Header("Scene Management")]
    public int sceneToLoad; // escena que se cargará al ganar

    [Header("Sound References")]

    public PlayerController playerCont;


    void Start()
    {
        points = 0; // Inicializa los puntos del jugador a 0 al inicio del juego
    }

    void Update()
    {
        if (points >= winPoints) // si los puntos del jugador son mayores o iguales a los puntos necesarios para ganar
        {
            LoadScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            points++; // Incrementa los puntos del jugador en 1
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false); // desactiva el objeto que el jugador ha recogido
            playerCont.PlaySFX(2); // llama al método PlaySFX del otro Script para reproducir un efecto de sonido
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad); // carga la escena especificada en sceneToLoad
    }
}