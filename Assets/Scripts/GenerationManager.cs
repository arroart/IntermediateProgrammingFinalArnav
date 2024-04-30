using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class GenerationManager : MonoBehaviour
{
    [SerializeField] PrefabDatabase prefabDB;
    [SerializeField] Transform pathGroup;

    public static GenerationManager _instance;



    //List<BoxLog> boxLog = new List<BoxLog>();

    //Stack for Recursive backtracker
    public List<PathBox> pathBoxes = new List<PathBox>();
    //int GenerationCount;

    int currentGcount;
    //PathBox[] addedGen;

    List<Vector2> logPos = new List<Vector2>();

   
    
    // Start is called before the first frame update
    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 
    void Start()
    {
        //addedGen = new PathBox[5];
        GenerateFirst();
        Invoke("BuildNav", 5);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RemoveBoxes(1);
        }
    }

    public void GenerateFirst()
    {
       
        for (int i = 0; i < 4; i++)
        {
            PathBox currentBox = Instantiate(prefabDB.prefabList[0], pathGroup).GetComponent<PathBox>();
            currentBox.Position = new Vector3(0, 0, currentBox.mazeSize*(i));
            currentBox.walls[2].active = false;
            currentBox.walls[3].active = false;

            if (i == 0)
            {
                currentBox.walls[2].active = true;
            }
            if (i == 3)
            {
                currentBox.walls[3].active = true;
            }
            currentBox.Init(0, i);
            pathBoxes.Add(currentBox);
            logPos.Add(new Vector2(0, i));
        }
        
        GeneratePath(20,0);
    }

    public void GeneratePath(int boxCount, int remCount)
    {
        
        if (currentGcount < boxCount)
        {
            //addedGen[currentGcount] = baseBox;
            int spawnSide = Random.Range(0, 4);
            Debug.Log(spawnSide);
            PathBox spawnedBox = Instantiate(prefabDB.prefabList[0], pathGroup).GetComponent<PathBox>();
            PathBox previousBox = pathBoxes[pathBoxes.Count-1];
            Vector2 spawnPos = new Vector2(0, 0);

            //aa
            switch (spawnSide)
            {
                case 0:
                    spawnPos = new Vector2(previousBox.locX - 1, previousBox.locY);
                    if (LogExists(spawnPos))
                    {
                       
                        
                        break;
                        
                    }
;                    spawnedBox.Position = new Vector3(previousBox.Position.x - spawnedBox.mazeSize, 0, previousBox.Position.z);
                    spawnedBox.walls[0].active = false;
                    previousBox.walls[1].active = false;
                    
                    
                    break;

                case 1:
                    spawnPos= new Vector2(previousBox.locX + 1, previousBox.locY);
                    if (LogExists(spawnPos))
                    {
                        
                        
                        break;
                    }
                    spawnedBox.Position = new Vector3(previousBox.Position.x + spawnedBox.mazeSize, 0, previousBox.Position.z);
                    spawnedBox.walls[1].active = false;
                    previousBox.walls[0].active = false;
              
                    break;

                case 2:
                    spawnPos = new Vector2(previousBox.locX, previousBox.locY+1);
                    if (LogExists(spawnPos))
                    {
                        
                        
                        break;
                    }
                    spawnedBox.Position = new Vector3(previousBox.Position.x, 0, previousBox.Position.z + spawnedBox.mazeSize);
                    spawnedBox.walls[2].active = false;
                    previousBox.walls[3].active = false;
                    
                    break;

                case 3:
                    spawnPos = new Vector2(previousBox.locX, previousBox.locY-1);
                    if (LogExists(spawnPos))
                    {
                        
                        break;
                    }
                    spawnedBox.Position = new Vector3(previousBox.Position.x, 0, previousBox.Position.z - spawnedBox.mazeSize);
                    spawnedBox.walls[3].active = false;
                    previousBox.walls[2].active = false;
                    
                    break;



            }

            

            if (LogExists(spawnPos))
            {
                Destroy(spawnedBox.gameObject);
                GeneratePath(boxCount, remCount);
            }
            else
            {

                int randomChance = Random.Range(0, 9);
                if (randomChance < 3)
                {

                    spawnedBox.obstacles[randomChance].active = true;

                    spawnedBox.obstacles[randomChance].transform.rotation = Quaternion.Euler(0, 90 * randomChance, 0);
                }
                if (randomChance % 3 == 0)
                {
                    spawnedBox.coinOb.active = true;
                }

                int randomWall = Random.Range(0, 7);
                if (randomWall < 4)
                {
                    spawnedBox.walls[randomWall].transform.position = new Vector3(spawnedBox.walls[randomWall].transform.position.x, spawnedBox.walls[randomWall].transform.position.y - 2f - 1f, spawnedBox.walls[randomWall].transform.position.z);
                    spawnedBox.walls[randomWall].GetComponent<BoxCollider>().isTrigger = true;
                }

                if (randomWall == 5)
                {
                    if (!previousBox.bounceBall.active)
                    {
                        spawnedBox.bounceBall.active = true;

                        spawnedBox.bounceBall.transform.localScale = spawnedBox.bounceBall.transform.localScale * Random.Range(0.8f, 1.2f);
                    }

                }

                
                spawnedBox.Init((int)spawnPos.x, (int)spawnPos.y);
                logPos.Add(spawnPos);
                pathBoxes.Add(spawnedBox);
                currentGcount++;
                GeneratePath(boxCount, remCount);
            }

            
            
        }
        else
        {
            currentGcount = 0;
            pathBoxes[pathBoxes.Count - (int)boxCount / 2].genTrigger.active = true;

            if (remCount != 0)
            {
                RemoveBoxes((int)boxCount);
            }
            
            
            
            Invoke("BuildNav",1f);


        }
            
    }

    private bool LogExists(Vector2 checkingValue)
    {
        for(int i=0; i < logPos.Count; i++)
        {
            if(logPos[i].x==checkingValue.x && logPos[i].y == checkingValue.y)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveBoxes(int boxCount)
    {

        GameManager.gm.enemy.transform.position = new Vector3(pathBoxes[pathBoxes.Count - 8].Position.x, 1, pathBoxes[pathBoxes.Count - boxCount].Position.z);
        for (int i = 0; i < boxCount; i++)
        {
            PathBox boxRemoved = pathBoxes[0];
            pathBoxes.Remove(boxRemoved);
            logPos.Remove(new Vector2(boxRemoved.locX, boxRemoved.locY));

            //if (i == boxCount - 1)
            //{
            //    for (int j = 0; j < boxRemoved.walls.Length; j++)
            //    {
            //        if (j == boxRemoved.walls.Length - 1)
            //        {
            //            if (!boxRemoved.walls[j].active && !pathBoxes[0].walls[0].active)
            //            {
            //                pathBoxes[0].walls[0].active = true;
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            if (!boxRemoved.walls[j].active && !pathBoxes[0].walls[j + 1].active)
            //            {
            //                pathBoxes[0].walls[j + 1].active = true;
            //                break;
            //            }
            //        }

            //    }
            //}


            Destroy(boxRemoved.gameObject);
        }

        
        
    }

    public void BuildNav()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

     
  
}

