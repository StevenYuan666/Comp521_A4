using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// The main script for the squirrel to initialize the goal states and the world states
public class Squirrel : MonoBehaviour, GOAPInterface{
    public SquirrelResources resources;
    public float moveSpeed = 1f;
    // public GameObject character;

    public static List<TreeComponent> taken = new List<TreeComponent>();

    void Start(){
        Invoke("GotHome", 0.005f);
    }

    void GotHome(){
        TreeComponent[] trees = (TreeComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(TreeComponent));
        TreeComponent targetHome = trees[Random.Range(0, 10)];
        while(taken.Contains(targetHome)){
            targetHome = trees[Random.Range(0, 10)];
        }
        taken.Add(targetHome);
        resources.homeTree = targetHome;
        this.gameObject.transform.position = resources.homeTree.transform.position + new Vector3(0.3f, 1f, 0);
    }

    void Update(){
    }

    public HashSet<KeyValuePair<string, object>> getWorld(){
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("hasTrash", false));
        // "hasFirstNuts", true
        worldData.Add(new KeyValuePair<string, object>("hasFirstNuts", false));
        worldData.Add(new KeyValuePair<string, object>("hasSecondNuts", false));
        worldData.Add(new KeyValuePair<string, object>("hasThirdNuts", false));
        // Do any other actions should turn the roaming status back to false as well
        worldData.Add(new KeyValuePair<string, object>("Roaming", false));
        // Debug.Log("Distance " + Vector3.Distance(this.gameObject.transform.position, character.transform.position));
        // Debug.Log(Vector3.Distance(this.gameObject.transform.position, character.transform.position) < 3f);
        // worldData.Add(new KeyValuePair<string, object>("NeedSeekingRefuge", Vector3.Distance(this.gameObject.transform.position, character.transform.position) < 3f));
        // worldData.Add(new KeyValuePair<string, object>("SeekingRefuge", false));
        return worldData;
    }

    public HashSet<KeyValuePair<string, object>> createGoal(){
        HashSet<KeyValuePair<string, object>> goals = new HashSet<KeyValuePair<string, object>>();
        goals.Add(new KeyValuePair<string, object>("collectNuts", true));
        goals.Add(new KeyValuePair<string, object>("collectTrash", true));
        goals.Add(new KeyValuePair<string, object>("Roaming", true));
        // goals.Add(new KeyValuePair<string, object>("SeekingRefuge", true));
        return goals;
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failed){
        Debug.Log("Failed");
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<Action> actions){
        Debug.Log("Found");
    }

    public void actionsFinished(){
        Debug.Log("Action Done");
    }

    public void planAborted(Action abort){
        Debug.Log("Plan Aborted");
    }

    public bool move(Action next){
        float step = moveSpeed * Time.deltaTime;
        // Vector3.MoveTowards(gameObject.transform.position, next.body.transform.position, step);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z), step);
        // gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(next.body.transform.position.x, 0, next.body.transform.position.z), step);
        // gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, next.body.transform.position + new Vector3(0.3f, 0, 0), step);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, next.body.transform.position + new Vector3(0.3f, 0, 0), step);
        if(Vector3.Distance(gameObject.transform.position, next.body.transform.position + new Vector3(0.3f, 0, 0)) == 0f){
            next.setInRange(true);
            return true;
        }
        else{
            return false;
        }
    }
    
}