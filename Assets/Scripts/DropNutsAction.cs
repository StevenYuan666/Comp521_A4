using System;
using UnityEngine;

public class DropNutsAction : Action{
    private bool droppedNuts = false;
    private TreeComponent homeTree;

    // Define the action to drop all collected nuts
    public DropNutsAction(){
        addPreCondition("hasFirstNuts", true);
        addPreCondition("hasSecondNuts", true);
        addPreCondition("hasThirdNuts", true);
        addPostCondition("hasFirstNuts", false);
        addPostCondition("hasSecondNuts", false);
        addPostCondition("hasThirdNuts", false);
        addPostCondition("collectNuts", true);
        addPostCondition("Roaming", false);
    }

    public override void reset()
    {
        droppedNuts = false;
    }

    public override bool isFinished()
    {
        return droppedNuts;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    // The squirrel need to have a home tree so that it can drop all nuts at home
    public override bool checkPreConditions(GameObject agent)
    {
        SquirrelResources resources = (SquirrelResources)agent.GetComponent(typeof(SquirrelResources));
        homeTree = resources.homeTree;
        body = homeTree.gameObject;
        return homeTree != null;
    }

    // Drop all nuts
    public override bool perform(GameObject agent)
    {
        Debug.Log("Drop Nuts");
        SquirrelResources resources = (SquirrelResources)agent.GetComponent(typeof(SquirrelResources));
        droppedNuts = true;
        resources.numNuts = 0;
        return true;
    }


}