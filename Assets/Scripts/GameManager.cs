using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    [SerializeField]
    float timerDelay=10f;

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
        player =Instantiate(playerPrefab);
        sceneCamera.GetComponent<PlayerCam>().orientation = GameObject.Find("Orientation").transform;
        sceneCamera.GetComponentInParent<MoveCam>().CameraPos = GameObject.Find("CameraPos").transform;
        player.transform.position = new Vector3(0, 5, 0);

        Invoke("SpawnEnemy",1);
    }

    // Update is called once per frame
    void Update()
    {
       
        
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
            yield return new WaitForSeconds(timerDelay);
            gameCount++;

            if (gameCount % 5 == 0)
            {
                enemy.GetComponent<EnemyFollow>().IncreaseSpeed(0.2f);
            }
        }
    }
}
