using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{

    public GameObject Tree;
    public GameObject Can;

    private List<Vector3> Trees = new List<Vector3>();
    private List<Vector3> Taken = new List<Vector3>();
    private List<Vector3> Cans = new List<Vector3>();
    private List<GameObject> CansObjects = new List<GameObject>();
    private List<GameObject> TreesObjects = new List<GameObject>();
    public GameObject Squirrel;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Update(){
        
    }

    // Used to initialize the garbage cans and all trees at the begining
    void Initialize(){
        for(int i = 0; i < 10; i ++){
            GenerateTree();
        }
        for(int i = 0; i < 5; i ++){
            GenerateCan();
        }
    }

    void GenerateTree(){
        Vector3 pos = new Vector3(Random.Range(-9, 10), 0, Random.Range(-9, 10));
        while(Taken.Contains(pos)){
            pos = new Vector3(Random.Range(-9, 10), 0, Random.Range(-9, 10));
        }
        Trees.Add(pos);
        Taken.Add(pos);
        Taken.Add(pos + new Vector3(1, 0, 0));
        Taken.Add(pos + new Vector3(-1, 0, 0));
        Taken.Add(pos + new Vector3(0, 0, 1));
        Taken.Add(pos + new Vector3(0, 0, -1));
        GameObject tree = Instantiate(Tree, pos, Quaternion.identity);
        TreesObjects.Add(tree);
    }

    void GenerateCan(){
        Vector3 pos = new Vector3(Random.Range(-9, 10), 0, Random.Range(-9, 10));
        while(Taken.Contains(pos)){
            pos = new Vector3(Random.Range(-9, 10), 0, Random.Range(-9, 10));
        }
        Cans.Add(pos);
        Taken.Add(pos);
        GameObject can = Instantiate(Can, pos + new Vector3(0, 0.25f, 0), Quaternion.identity);
        CansObjects.Add(can);
    }

}
