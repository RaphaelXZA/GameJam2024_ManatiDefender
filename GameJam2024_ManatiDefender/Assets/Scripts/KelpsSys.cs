using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace kelp_eater
{
    public class KelpsSys : MonoBehaviour
    {
        [SerializeField] Button eatButton;
        [SerializeField] private kelp_eater.PlayerMove player;
        [SerializeField] private GameObject[] kelpArray;
        [SerializeField] private float yIncreaseAmount = 1f;
        [SerializeField] private float timer;
        [SerializeField] private float maxTimer = 2f;
        [SerializeField] private float timerMinimunRange;
        [SerializeField] private float timerMaximunRange;
        [SerializeField] private float timerDecreaseAmount;

        [SerializeField] float moveInterval = 0.6f; // Intervalo entre movimientos

        ScoreCounter scoreScript;

        void Start()
        {
            player = GameObject.Find("PlayerManatee").GetComponent<kelp_eater.PlayerMove>();
            scoreScript = GameObject.Find("ScoreManager").GetComponent<kelp_eater.ScoreCounter>();

            if (player == null)
            {
                Debug.LogError("PlayerManatee not found or PlayerMove component missing.");
                return;
            }

            StartCoroutine(KelpDown());

        }


        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= maxTimer)
            {
                UpKelp();
                timer = 0;
            }

            if (timerMaximunRange <= 0.01f)
            {
                timerMaximunRange = 0.01f;
            }

            if (timerMinimunRange <= 0.01f)
            {
                timerMinimunRange = 0.01f;
            }
            

        }


        void UpKelp()
        {
            maxTimer = Random.Range(timerMinimunRange, timerMaximunRange);
            int randomIndex = Random.Range(0, kelpArray.Length);
            GameObject randomKelp = kelpArray[randomIndex];

            Debug.Log("Selected random kelp: " + randomKelp.name);

            randomKelp.transform.position = new Vector3(
                randomKelp.transform.position.x,
                randomKelp.transform.position.y + yIncreaseAmount,
                randomKelp.transform.position.z);
        }

        IEnumerator KelpDown()
        {
            while (true)
            {
                for (int i = 0; i < kelpArray.Length; i++)
                {
                    kelpArray[i].transform.position = new Vector3(kelpArray[i].transform.position.x, Mathf.Clamp(kelpArray[i].transform.position.y, -14, 0), kelpArray[i].transform.position.z);
                }

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

                yield return new WaitForSeconds(moveInterval);
            }
        }

        public void DecreaseTimerRange()
        {
            timerMinimunRange -= timerDecreaseAmount;
            timerMaximunRange -= timerDecreaseAmount;
        }

    }
}
