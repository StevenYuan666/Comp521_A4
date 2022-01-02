using System;
using UnityEngine;
using System.Collections;


public class PickTrashAction : Action{

    private bool picked = false;
    private TrashComponent targetTrash;

    private float startTime = 0f;
    private float stuckDuration = 0.5f;

    // WE can only pick the trash if we don't pick any nut up
    public PickTrashAction(){
        this.cost = 0;
        addPreCondition("hasTrash", false);
        addPreCondition("hasFirstNuts", false);
        addPostCondition("hasTrash", true);
        addPostCondition("Roaming", false);
    }

    public override void reset()
    {
        picked = false;
        targetTrash = null;
        startTime = 0f;
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
        TrashComponent[] trashes = (TrashComponent[]) UnityEngine.GameObject.FindObjectsOfType(typeof(TrashComponent));
        TrashComponent closest = null;
        float dist = 0f;

        foreach(TrashComponent t in trashes){
            if(closest == null){
                closest = t;
                dist = Vector3.Distance(t.gameObject.transform.position, agent.transform.position);
            }
            else{
                float temp = Vector3.Distance(t.gameObject.transform.position, agent.transform.position);
                if(temp < dist){
                    closest = t;
                    dist = temp;
                }
            }
        }
        if(closest == null){
            return false;
        }


        // targetTrash = closest;
        targetTrash = trashes[UnityEngine.Random.Range(0, 5)];
        body = targetTrash.gameObject;
        return closest != null;
    }

    public override bool perform(GameObject agent)
    {
        // If found a trash
        if(!targetTrash.IsEmpty()){
            Debug.Log("Picked A Trash");
            SquirrelResources resourcses = (SquirrelResources)agent.GetComponent(typeof(SquirrelResources));
            targetTrash.setStatus(true);
            resourcses.haveTrash = true;
            picked = true;
            return true;
        }
        // If not found a trash
        else{
            SquirrelResources resourcses = (SquirrelResources)agent.GetComponent(typeof(SquirrelResources));
            resourcses.haveTrash = false;
            picked = false;

            Debug.Log("Not Found a Trash");
            startTime = 0;
            while(startTime < stuckDuration){
                startTime += Time.deltaTime;
            }
            // StartCoroutine(waiter());
            return false;
        }
    }

    IEnumerator waiter(){
        
        yield return new WaitForSecondsRealtime(2);
    }


}