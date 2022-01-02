using System;
using UnityEngine;

public class RoamingAction : Action {

    private bool roaming = false;
    private GameObject target;

    // One of the idle state, the squirrel will only roam at all, actually do nothing
    public RoamingAction(){
        this.cost = 0;
        // new KeyValuePair<string, object>("Roaming", true)
        addPreCondition("Roaming", false);
        addPostCondition("Roaming", true);
    }

    public override void reset()
    {
        roaming = false;
    }

    public override bool isFinished()
    {
        return roaming;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override bool checkPreConditions(GameObject agent)
    {
        target = new GameObject();
        target.transform.position = new Vector3(UnityEngine.Random.Range(-9.5f, 9.5f), 0f, UnityEngine.Random.Range(-9.5f, 9.5f));
        body = target;
        return target != null;
    }

    public override bool perform(GameObject agent)
    {
        Debug.Log("roaming");
        /*
        float step = 1f * Time.deltaTime;
        roaming = true;
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, target.transform.position, step);
        */
        roaming = true;
        return true;
    }



}