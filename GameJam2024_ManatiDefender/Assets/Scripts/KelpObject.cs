using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace kelp_eater
{

    public class KelpObject : MonoBehaviour
    {
        GameManager gameManagerScript;
        TutorialHandler tutorialHandler;
        [SerializeField] Material  originalMaterial;
        [SerializeField] Material redMaterial;
        [SerializeField] SkinnedMeshRenderer objectRenderer;

        private void Start()
        {
            tutorialHandler = FindAnyObjectByType<TutorialHandler>();
            objectRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            
            originalMaterial = objectRenderer.material;

            gameManagerScript = GameObject.Find("GameController").GetComponent<GameManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Lose"))
            {
                Debug.Log("Colisiono con la zona de perdida");
                gameManagerScript.GameOverAndContinue();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Behavior"))
            {
                objectRenderer.material = redMaterial;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            objectRenderer.material = originalMaterial;
        }
    }

}
