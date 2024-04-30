using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    Vector3 OGpos;
    Vector3 updatedPos;

    bool goUp = true;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        OGpos = transform.position;
        updatedPos= new Vector3(OGpos.x, OGpos.y + 5, OGpos.z);

        speed = Random.Range(0.1f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if(goUp)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);

            if (transform.position.y >= updatedPos.y)
            {
                goUp = false;
            }
        }
        
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y -(speed * Time.deltaTime), transform.position.z);
            if (transform.position.y <= OGpos.y)
            {
                goUp = true;
            }
        }

        var lookDir = GameManager.gm.player.transform.position - transform.position;
        lookDir.y = 0;
        
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "PlayerP")
        {
            GameManager.gm.coinCount++;
            gameObject.active = false;
        }
    }
}
