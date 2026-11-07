using UnityEngine;


public class KnifeTrap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed;
    public Transform[] points;
    public int startingPoint;
    private int i;

    public Transform spawnPoint; // arrastrar el objeto aqui para que sea punto de referencia
    void Start()
    {
        transform.position = points[startingPoint].position;

        i = startingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, points[i].position) < 0.5f)
        {
            i++;
            if (i == points.Length) //chequea si la plataforma llega al último punto
            {
                i = 0; // resetea
            }
        }


        transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    // collision letal

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (spawnPoint != null) 
            {
                // En 3D se suele usar Vector3 para la posición
                collision.gameObject.transform.position = spawnPoint.position;
            }
        }
    }

}
