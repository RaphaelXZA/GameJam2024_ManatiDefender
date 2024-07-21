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

        [SerializeField] private GameObject kelpParticle;

        public float downAmount = 1;

        public float eatInterval = 0.6f; 

        ScoreCounter scoreScript;

        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<kelp_eater.PlayerMove>();
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
            PlayerMove playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
            while(true)
            {
                for (int i = 0; i < kelpArray.Length; i++)
                {
                    kelpArray[i].transform.position = new Vector3(kelpArray[i].transform.position.x, Mathf.Clamp(kelpArray[i].transform.position.y, -13, -5), kelpArray[i].transform.position.z);
                }

                switch (player.currentZoneIndex)
                {
                    case 0:
                        Instantiate(kelpParticle, playerMove.transform.position+Vector3.up,Quaternion.identity);
                        kelpArray[0].transform.position -= new Vector3(0, downAmount, 0);
                        break;

                    case 1:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        kelpArray[1].transform.position -= new Vector3(0, downAmount, 0);
                        break;

                    case 2:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        kelpArray[2].transform.position -= new Vector3(0, downAmount, 0);
                        break;

                    case 3:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        kelpArray[3].transform.position -= new Vector3(0, downAmount, 0);
                        break;

                    case 4:
                        Instantiate(kelpParticle, playerMove.transform.position + Vector3.up, Quaternion.identity);
                        kelpArray[4].transform.position -= new Vector3(0, downAmount, 0);
                        break;

                    default:
                        break;
                }

                yield return new WaitForSeconds(eatInterval);
            }
        }

        public void DecreaseTimerRange()
        {
            timerMinimunRange -= timerDecreaseAmount;
            timerMaximunRange -= timerDecreaseAmount;
            downAmount += 0.2f;
        }

    }
}
