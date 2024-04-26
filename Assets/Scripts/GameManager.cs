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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
