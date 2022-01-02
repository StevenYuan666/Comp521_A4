using System;
using UnityEngine;

public class DropTrashAction : Action{
    private bool droppedTrash = false;
    private TreeComponent homeTree;

    // Similar to the idea of dropping nuts, but drop the trash collected from garbage can here
    public DropTrashAction(){
        addPreCondition("hasTrash", true);
        addPostCondition("hasTrash", false);
        addPostCondition("collectTrash", true);
        addPostCondition("Roaming", false);
    }

    public override void reset()
    {
        droppedTrash = false;
        //
    }

    public override bool isFinished()
    {
        return droppedTrash;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override bool checkPreConditions(GameObject agent)
    {
        SquirrelResources resources = (SquirrelResources)agent.GetComponent(typeof(SquirrelResources));
        homeTree = resources.homeTree;
        body = homeTree.gameObject;
        return homeTree != null;
    }

    public override bool perform(GameObject agent)
    {
        Debug.Log("Drop Trash");
        SquirrelResources resources = (SquirrelResources)agent.GetComponent(typeof(SquirrelResources));
        droppedTrash = true;
        resources.haveTrash = false;
        return true;
    }


}