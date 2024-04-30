using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFollow : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerP")
        {
            GameManager.gm.SceneLoad();
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(GameManager.gm.player.transform.position.x, -20, GameManager.gm.player.transform.position.z);
    }

   
}
