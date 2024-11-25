using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float rotationSpeed = 10f; // Velocidad de rotación

    void Update()
    {
        // Movimiento hacia adelante
        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;
        transform.position += forwardMovement;

        // Rotación hacia el nuevo objetivo basado en inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculamos la nueva dirección basada en el input
        Vector3 newDirection = new Vector3(horizontalInput, 0f, verticalInput);

        if (newDirection != Vector3.zero)
        {
            // Rotación suave hacia la nueva dirección
            Quaternion targetRotation = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
