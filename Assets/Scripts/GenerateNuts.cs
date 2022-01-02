using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used by all tree to generate nuts
public class GenerateNuts : MonoBehaviour
{
    public GameObject Nut;
    float NutTimer = 2f;
    public int count = 0;
    public List<GameObject> AllNutsObjects = new List<GameObject>();
    public List<Vector3> AllNuts = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // The count should be update every frame, since the nuts can be collected by squirrel
        // or destroyed by the player in ghost mode
        count = AllNutsObjects.Count;
        NutTimer -= Time.deltaTime;
        // Generate a nut for each tree every two seconds
        if (NutTimer <= 0)
        {
            UpdateNuts();
            NutTimer = 2f;
        }
    }

    void UpdateNuts(){
        if(count >= 5){
            return;
        }

        // Ensure the generated nuts will not be covered by the trunk
        int choice = Random.Range(0, 4);
        Vector3 pos;
        if(choice == 0){
            pos = gameObject.transform.position + new Vector3(Random.Range(-0.8f, -0.3f), 0, Random.Range(-0.8f, -0.3f));
            while(AllNuts.Contains(pos)){
            pos = gameObject.transform.position + new Vector3(Random.Range(-0.8f, -0.3f), 0, Random.Range(-0.8f, -0.3f));
            }
        }
        else if(choice == 1){
            pos = gameObject.transform.position + new Vector3(Random.Range(-0.8f, -0.3f), 0, Random.Range(0.3f, 0.8f));
            while(AllNuts.Contains(pos)){
            pos = gameObject.transform.position + new Vector3(Random.Range(-0.8f, -0.3f), 0, Random.Range(0.3f, 0.8f));
            }
        }
        else if(choice == 2){
            pos = gameObject.transform.position + new Vector3(Random.Range(0.3f, 0.8f), 0, Random.Range(-0.8f, -0.3f));
            while(AllNuts.Contains(pos)){
            pos = gameObject.transform.position + new Vector3(Random.Range(0.3f, 0.8f), 0, Random.Range(-0.8f, -0.3f));
            }
        }
        else{
            pos = gameObject.transform.position + new Vector3(Random.Range(0.3f, 0.8f), 0, Random.Range(0.3f, 0.8f));
            while(AllNuts.Contains(pos)){
            pos = gameObject.transform.position + new Vector3(Random.Range(0.3f, 0.8f), 0, Random.Range(0.3f, 0.8f));
            }
        }
        
        AllNuts.Add(pos);
        GameObject nut = Instantiate(Nut, pos + new Vector3(0, 0.05f, 0), Quaternion.identity);
        AllNutsObjects.Add(nut);
        count ++;
    }
}
