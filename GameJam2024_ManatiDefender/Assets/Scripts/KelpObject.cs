using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace kelp_eater
{    

    public class KelpObject : MonoBehaviour
    {
        GameManager gameManagerScript;

        [SerializeField] Material kelpMaterials;

        private void Start()
        {
            kelpMaterials = GetComponent<Material>();
            gameManagerScript = GameObject.Find("GameController").GetComponent<GameManager>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Lose"))
            {
                Debug.Log("Colisiono con la zona de perdida");
                gameManagerScript.GameOverAndContinue();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (collision.gameObject.CompareTag("Behavior"))
            {
                 
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            
        }

    }
}
