using UnityEngine;

public class AutoSensor : MonoBehaviour
{
    public float detectionDistance = 5f; // Longitud de la caja
    public float detectionWidth = 2f;   // Ancho de la caja
    public float detectionHeight = 2f;  // Altura de la caja
    public LayerMask detectionLayer;    // Capas que el sensor detectará
    [SerializeField] private bool isObstacleDetected = false;

    public float DistanceObstacle;
    public bool IsObstacleDetected => isObstacleDetected;
    Vector3 obstacle;

    private void Awake()
    {
        // Configura automáticamente el BoxCollider
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector3(detectionWidth, detectionHeight, detectionDistance);
            boxCollider.center = new Vector3(0, 0, detectionDistance / 2);
        }
        obstacle = transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto en la caja tiene el tag "citizen" y está en la capa correcta
        if (other.CompareTag("citizen") && (detectionLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            isObstacleDetected = true;
            DistanceObstacle = (other.gameObject.transform.position - transform.position).magnitude;
            obstacle = other.gameObject.transform.position;
           Debug.Log("DistanceObstacle: " + DistanceObstacle);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el objeto sale del área, el auto ya no detecta un obstáculo
        if (other.CompareTag("citizen") && (detectionLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            isObstacleDetected = false;
            DistanceObstacle = 0;
            obstacle = transform.position;
            Debug.Log("DistanceObstacle: " + DistanceObstacle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isObstacleDetected ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, obstacle);
        // Visualizar la caja en el editor
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.forward * (detectionDistance / 2),
                            new Vector3(detectionWidth, detectionHeight, detectionDistance));
    }
}
