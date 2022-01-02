using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The interface to ensure any agent implement the GOAP have the same required functions
public interface GOAPInterface{
    HashSet<KeyValuePair<string, object>> getWorld();
    HashSet<KeyValuePair<string, object>> createGoal();
    void planFailed(HashSet<KeyValuePair<string, object>> failed);
    void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<Action> actions);
    void actionsFinished();
    void planAborted(Action abort);
    bool move(Action next);
}
