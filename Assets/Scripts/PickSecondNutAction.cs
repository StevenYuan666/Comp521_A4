using System;
using UnityEngine;

public class PickSecondNutAction : Action {

    private bool picked = false;
    private NutComponent targetNut;

    // public SquirrelResources r;

    // private float startTime = 0f;
    // public float pickDuration = 0.5f;

    // Similar to picking the first nut, but with more strict pre conditions
    public PickSecondNutAction(){
        addPreCondition("hasTrash", false);
        addPreCondition("hasFirstNuts", true);
        addPreCondition("hasSecondNuts", false);
        addPreCondition("hasThirdNuts", false);
        addPostCondition("hasFirstNuts", true);
        addPostCondition("hasSecondNuts", true);
        addPostCondition("hasThirdNuts", false);
        addPostCondition("Roaming", false);
    }

    public override void reset()
    {
        picked = false;
    }

    public override bool isFinished()
    {
        return picked;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override bool checkPreConditions(GameObject agent)
    {
        NutComponent[] nuts = (NutComponent[]) UnityEngine.GameObject.FindObjectsOfType(typeof(NutComponent));
        NutComponent closest = null;
        float dist = 0f;

        foreach(NutComponent n in nuts){
            if(closest == null){
                closest = n;
                dist = Vector3.Distance(n.gameObject.transform.position, agent.transform.position);
            }
            else{
                float temp = Vector3.Distance(n.gameObject.transform.position, agent.transform.position);
                if(temp < dist){
                    closest = n;
                    dist = temp;
                }
            }
        }
        if(closest == null){
            return false;
        }


        // targetTrash = closest;
        targetNut = closest;
        body = targetNut.gameObject;
        return closest != null;
    }

    public override bool perform(GameObject agent)
    {
        Debug.Log("Picked A Nut");
        SquirrelResources resourcses = (SquirrelResources)agent.GetComponent(typeof(SquirrelResources));
        
        GenerateNuts[] trees = (GenerateNuts[]) UnityEngine.GameObject.FindObjectsOfType(typeof(GenerateNuts));
        GenerateNuts theTree = null;
        foreach(GenerateNuts t in trees){
            if(t.AllNutsObjects.Contains(body)){
                theTree = t;
            }
        }
        if(theTree != null){
            theTree.count --;
        }
        
        Destroy(body);
        resourcses.numNuts ++;
        picked = true;
        return true;
    }



}