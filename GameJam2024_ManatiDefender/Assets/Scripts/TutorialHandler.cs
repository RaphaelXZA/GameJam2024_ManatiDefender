using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kelp_eater
{
    public class TutorialHandler : MonoBehaviour
    {
        public GameObject GameplayObject;  // Objeto que se activar�

        void Awake()
        {
            if (this.gameObject == null || GameplayObject == null)
            {
                Debug.LogError("Tutorial o Gameplay son nulos.");
                return;
            }
            GameplayObject.SetActive(false); // Aseg�rate de que el objeto Gameplay est� inactivo al inicio
        }

        void Update()
        {
            // Detectar un toque en la pantalla
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                
                GameplayObject.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
