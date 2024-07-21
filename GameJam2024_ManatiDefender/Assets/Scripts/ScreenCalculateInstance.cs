using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kelp_eater
{
    public class ScreenCalculateInstance : MonoBehaviour
    {
        public GameObject[] objectPrefabs; // Prefabricado del objeto que quieres instanciar
        public GameObject[] squarePrefabs; // Prefabricado del cuadrado que quieres instanciar en la parte superior
        public GameObject[] voids;
        public GameObject[] rocks;

        public int numberOfObjects = 5; // Número de objetos que quieres instanciar
        public float marginTop = 0.5f; // Margen en unidades de World Space desde el borde superior de la pantalla
        public float marginBottom = 0.5f; // Margen en unidades de World Space desde el borde inferior de la pantalla

        void Start()
        {
            ArrangeObjects();
            ArrangeSquares();
            ArrangeBottomObjects();
            ArrangeBottomRocks();
        }

        public void ArrangeObjects()
        {
            float screenHeightInWorldUnits = Camera.main.orthographicSize * 2;
            float screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;

            float startX = -screenWidthInWorldUnits / 2 + marginTop * 6;
            float endX = screenWidthInWorldUnits / 2 - marginTop * 6;

            float spaceBetweenObjects = (endX - startX) / (numberOfObjects - 1);

            for (int i = 0; i < numberOfObjects && i < objectPrefabs.Length; i++)
            {
                float posX = startX + i * spaceBetweenObjects;
                Vector3 spawnPosition = new Vector3(posX, -13, 0.08999991f); 

                GameObject objectToArrange = objectPrefabs[i];

                objectToArrange.transform.position = spawnPosition;
            }
        }

        void ArrangeSquares()
        {
            float screenHeightInWorldUnits = Camera.main.orthographicSize * 2;
            float screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;

            float squareWidth = screenWidthInWorldUnits - 2 * marginTop; 
            Vector3 squareScale = new Vector3(squareWidth, 1f, 1f); 

            Vector3 squarePosition = new Vector3(0f, screenHeightInWorldUnits / 2 + 0.5f, 2.34f);

            if (squarePrefabs.Length > 0)
            {
                GameObject squareToArrange = squarePrefabs[0];
                squareToArrange.transform.position = squarePosition;
                squareToArrange.transform.localScale = squareScale;
            }
        }

        void ArrangeBottomObjects()
        {
            float screenHeightInWorldUnits = Camera.main.orthographicSize * 2;
            float screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;

            float startX = -screenWidthInWorldUnits / 2 + marginBottom * 6;
            float endX = screenWidthInWorldUnits / 2 - marginBottom * 6;

            float spaceBetweenObjects = (endX - startX) / (numberOfObjects - 1);

            float screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;

            for (int i = 0; i < numberOfObjects && i < voids.Length; i++)
            {
                float posX = startX + i * spaceBetweenObjects;
                Vector3 spawnPosition = new Vector3(posX, screenBottom + 1.2f, 0); 

                GameObject objectToArrange = voids[i];

                objectToArrange.transform.position = spawnPosition;
            }
        }

        void ArrangeBottomRocks()
        {
            float screenHeightInWorldUnits = Camera.main.orthographicSize * 2;
            float screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;

            float startX = -screenWidthInWorldUnits / 2 + marginBottom * 6;
            float endX = screenWidthInWorldUnits / 2 - marginBottom * 6;

            float spaceBetweenObjects = (endX - startX) / (numberOfObjects - 1);

            float screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;

            for (int i = 0; i < numberOfObjects && i < rocks.Length; i++)
            {
                float posX = startX + i * spaceBetweenObjects;
                Vector3 spawnPosition = new Vector3(posX, screenBottom + 0f, 0.09f);

                GameObject objectToArrange = rocks[i];

                objectToArrange.transform.position = spawnPosition;
            }
        }
    }
}

