using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace kelp_eater
{
    public class KelpObject : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Lose"))
            {
                Debug.Log("Colisiono con la zona de perdida");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
