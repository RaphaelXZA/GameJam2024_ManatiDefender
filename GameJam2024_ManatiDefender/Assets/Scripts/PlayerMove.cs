using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencias a los GameObjects de las zonas
    public GameObject[] zoneObjects;
    private int currentZoneIndex = 2;  // Empezamos en la zona central (Zona 3)

    // Sensibilidad del deslizamiento
    public float swipeThreshold = 10f;

    void Update()
    {
        // Verifica la entrada táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchDeltaPosition = touch.deltaPosition;

            if (touch.phase == TouchPhase.Moved)
            {
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
                }
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
