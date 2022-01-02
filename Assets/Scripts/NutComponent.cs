using UnityEngine;
using System.Collections;

public class NutComponent : MonoBehaviour{

    // Initially used to handle the collection made by squirrels
    // Unfortunately, it does not work

    /*
    void OnCollisionEnter(){
        GenerateNuts[] trees = (GenerateNuts[]) UnityEngine.GameObject.FindObjectsOfType(typeof(GenerateNuts));
        GenerateNuts theTree = null;
        foreach(GenerateNuts t in trees){
            if(t.AllNutsObjects.Contains(gameObject)){
                theTree = t;
            }
        }
        if(theTree != null){
            theTree.count --;
        }
        // Destroy(gameObject);
    }
    */
}