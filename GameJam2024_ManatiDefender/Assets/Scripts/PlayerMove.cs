using UnityEngine;
using System.Collections;

namespace kelp_eater
{
    public class PlayerMove : MonoBehaviour
    {
        //VARIABLES de las zonas a las que el jugador puede moverse
        public GameObject[] zoneObjects;
        public int currentZoneIndex;

        public float swipeThreshold = 10f; //Sensibilidad del deslizamiento

        //VARIABLES para el comportamiento del deslizamiento
        private Vector2 startTouchPosition;
        private bool hasMoved = false;

        //VARIABLES de la secuencia inicial
        public int initialPositionIndex;  //Puedes elegir la posici�n inicial desde el Inspector
        public float moveDuration = 1.0f;  //Duraci�n de la animaci�n de movimiento
        private bool isAnimating = true;  //Booleano que indica si estamos animando

        //Bool que indica si el jugador ha llegado a la posici�n inicial y puede empezar el juego
        public bool isAtInitialPosition = false;

        [SerializeField] private GameObject BubbleParticle;

        void Start()
        {
            //Mueve al jugador a la posici�n inicial al comenzar el juego
            StartCoroutine(MoveToStartPosition(initialPositionIndex));
        }

        void Update()
        {
            //Si esta en la secuencia, no puede deslizar
            if (isAnimating) return;

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
                        if (!hasMoved)
                        {
                            Vector2 touchDeltaPosition = touch.position - startTouchPosition;

                            if (Mathf.Abs(touchDeltaPosition.x) > swipeThreshold) //Verifica si el deslizamiento es suficiente
                            {
                                if (touchDeltaPosition.x > 0) //Derecha
                                {
                                    MoveToNextZone(true);
                                }
                                else if (touchDeltaPosition.x < 0) //Izquierda
                                {
                                    MoveToNextZone(false);
                                }
                                hasMoved = true;
                            }
                        }
                        break;
                }
            }
        }

        IEnumerator MoveToStartPosition(int targetIndex)
        {
            if (targetIndex < 0 || targetIndex >= zoneObjects.Length)
            {
                Debug.LogError("Posicion inicial invalida");
                yield break;
            }


            isAnimating = true; 

            Vector3 startPosition = transform.position;
            Vector3 endPosition = zoneObjects[targetIndex].transform.position;

            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveDuration));
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }


            transform.position = endPosition; 
            currentZoneIndex = targetIndex;
            isAtInitialPosition = true;
            isAnimating = false;
        }

        void MoveToNextZone(bool moveRight)
        {
            Transform player = GetComponent<Transform>();   
            Vector3 vector3 = player.localScale;
            Debug.Log(vector3);
            if (moveRight && currentZoneIndex < zoneObjects.Length - 1)
            {
                Instantiate(BubbleParticle, transform.position + Vector3.up, Quaternion.identity);
                player.localScale = new Vector3(-1, vector3.y, vector3.z);
                currentZoneIndex++;
            }
            else if (!moveRight && currentZoneIndex > 0)
            {
                Instantiate(BubbleParticle, transform.position + Vector3.up, Quaternion.identity);
                player.localScale = new Vector3(1, 1, 1);
                currentZoneIndex--;
            }

            SoundManager.PlaySpecificSound(SoundType.PLAYER_MOVE, 0, 1f);
            Vector3 newPosition = zoneObjects[currentZoneIndex].transform.position;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }
}


