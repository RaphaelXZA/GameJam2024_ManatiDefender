using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpsSys : MonoBehaviour
{
    [SerializeField] private kelp_eater.PlayerMove player;
    [SerializeField] private GameObject[] kelpArray;
    Touch touch;
    void Start()
    {
        player = GameObject.Find("PlayerManatee").GetComponent<kelp_eater.PlayerMove>();

        if (player == null)
        {
            Debug.LogError("PlayerManatee not found or PlayerMove component missing.");
            return;
        }

        
    }

    void Update()
    {
        //Corregir Input correcto a tap tap en la pantalla xd
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (player.currentZoneIndex)
            {
                case 0:
                    kelpArray[0].transform.position -= new Vector3(0, 1f, 0);
                    break;

                case 1:
                    kelpArray[1].transform.position -= new Vector3(0, 1f, 0);
                    break;

                case 2:
                    kelpArray[2].transform.position -= new Vector3(0, 1f, 0);
                    break;

                case 3:
                    kelpArray[3].transform.position -= new Vector3(0, 1f, 0);
                    break;

                case 4:
                    kelpArray[4].transform.position -= new Vector3(0, 1f, 0);
                    break;

                default:
                    break;
            }
        }
    }
}