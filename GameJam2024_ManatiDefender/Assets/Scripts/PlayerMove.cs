using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencias a los GameObjects de las zonas
    public GameObject[] zoneObjects;
    private int currentZoneIndex = 2;  // Empezamos en la zona central (Zona 3)

    // Sensibilidad del deslizamiento
    public float swipeThreshold = 10f;

    // Variables para el manejo del deslizamiento
    private Vector2 startTouchPosition;
    private bool hasMoved = false;

    void Update()
    {
        // Verifica la entrada táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    hasMoved = false;
                    break;

                case TouchPhase.Moved:
                    if (!hasMoved) // Asegura que solo procesamos el primer movimiento significativo
                    {
                        Vector2 touchDeltaPosition = touch.position - startTouchPosition;

                        if (Mathf.Abs(touchDeltaPosition.x) > swipeThreshold) // Verifica si el deslizamiento es suficiente
                        {
                            if (touchDeltaPosition.x > 0) // Desliza hacia la derecha
                            {
                                MoveToNextZone(true);
                            }
                            else if (touchDeltaPosition.x < 0) // Desliza hacia la izquierda
                            {
                                MoveToNextZone(false);
                            }
                            hasMoved = true; // Marca que hemos procesado el movimiento
                        }
                    }
                    break;
            }
        }
    }

    void MoveToNextZone(bool moveRight)
    {
        if (moveRight && currentZoneIndex < zoneObjects.Length - 1)
        {
            currentZoneIndex++;
        }
        else if (!moveRight && currentZoneIndex > 0)
        {
            currentZoneIndex--;
        }

        // Mueve al jugador a la nueva posición en el eje X
        Vector3 newPosition = zoneObjects[currentZoneIndex].transform.position;
        newPosition.z = transform.position.z; // Mantén la posición en el eje Z
        transform.position = newPosition;
    }
}
