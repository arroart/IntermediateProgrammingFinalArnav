using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GenerationManager._instance.GeneratePath(10,1);

        gameObject.active = false;
    }
}
