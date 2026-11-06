using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;

public class FloatingObjects : MonoBehaviour


{
    [Header("Floating Configuration")]
    public float sinkDistance = 0.5f;
    public float responseSpeed = 5f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private int playerInsideCount = 0;
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition;

    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * responseSpeed);
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        playerInsideCount++;
        targetPosition = startPosition - new Vector3(0, sinkDistance, 0);
    }
}
    
    private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        playerInsideCount--;
        if (playerInsideCount <= 0)
        {
            playerInsideCount = 0;
            targetPosition = startPosition;
        }
    }
    
}
}
