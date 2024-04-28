using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{

    [SerializeField]
    NavMeshAgent enemyMesh;

    bool navCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.player != null && enemyMesh.isOnNavMesh)
        {
            enemyMesh.SetDestination(GameManager.gm.player.transform.position);
        }

    }

    public void IncreaseSpeed(float a)
    {
        if (enemyMesh.speed < 40)
        {
            enemyMesh.speed += a;
        }
        
    }
}
