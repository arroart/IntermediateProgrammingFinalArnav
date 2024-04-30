using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
   

    public static GameManager gm;

    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    public GameObject sceneCamera;

    [System.NonSerialized]
    public GameObject player;
    [SerializeField]
    GameObject enemyPrefab;
    public GameObject enemy;

    public int gameCount;
    public int coinCount;

    int score;
    [SerializeField]
    float timerDelay=10f;

    [SerializeField]
    TextMeshProUGUI playerScore;

    [SerializeField]
    TextMeshProUGUI HighScore;

    [SerializeField]
    TextMeshProUGUI distanceScore;

    [SerializeField]
    TextMeshProUGUI coinScore;




    private void Awake()
    {

        if (gm== null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        player =Instantiate(playerPrefab);
        sceneCamera.GetComponent<PlayerCam>().orientation = GameObject.Find("Orientation").transform;
        sceneCamera.GetComponentInParent<MoveCam>().CameraPos = GameObject.Find("CameraPos").transform;
        player.transform.position = new Vector3(0, 5, 0);

        Invoke("SpawnEnemy",1);

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        score = gameCount + coinCount;
        if (score > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore",score);
        }

        coinScore.text= "Absorbed: " + coinCount.ToString();
        distanceScore.text = "Lasted: " + gameCount.ToString()+"s"+"";

        playerScore.text = "Score: " + score.ToString();
        HighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    public void SpawnEnemy()
    {
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(0, 10, 0); ;
        }
        
    }

    IEnumerator Timer()
    {
        
        while (true)
        {
            Debug.Log("aa");
            gameCount++;
            

            if (gameCount > 20 && gameCount % 20 == 0)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<EnemyFollow>().IncreaseSpeed(0.02f);
                }


            }
            yield return new WaitForSeconds(timerDelay);
            
        }
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
