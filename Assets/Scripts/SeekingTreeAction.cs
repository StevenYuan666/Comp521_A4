using System;
using UnityEngine;

// Initially try to use this action to enable the squirrel seek refuge, but unfortunately failed

public class SeekingTreeAction : Action {

    private bool seeking = false;
    private TreeComponent target;

    public GameObject character;

    public SeekingTreeAction(){
        this.cost = 0;
        addPreCondition("NeedSeekingRefuge", true);
        addPostCondition("SeekingRefuge", true);
        // (Vector3.Distance(this.gameObject.transform.position, character.transform.position) < 3f)
        addPostCondition("NeedSeekingRefuge", false);
    }

    public override void reset()
    {
        seeking = false;
    }

    public override bool isFinished()
    {
        return seeking;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override bool checkPreConditions(GameObject agent)
    {
        TreeComponent[] trees = (TreeComponent[]) UnityEngine.GameObject.FindObjectsOfType(typeof(TreeComponent));
        TreeComponent closest = null;
        float dist = 0f;

        foreach(TreeComponent t in trees){
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


        target = closest;
        body = target.gameObject;
        return closest != null;
    }

    public override bool perform(GameObject agent)
    {
        Debug.Log("seeking");
        /*
        float step = 1f * Time.deltaTime;
        seeking = true;
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, target.transform.position, step);
        */
        seeking = true;
        return true;
    }



}