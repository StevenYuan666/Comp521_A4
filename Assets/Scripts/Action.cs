using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The abstract action class used to define all actions
public abstract class Action : MonoBehaviour
{
    // Pre conditions of the action
    private HashSet<KeyValuePair<string, object>> preConditions;
    // Post conditions of the action
    private HashSet<KeyValuePair<string, object>> postConditions;
    // Check if the agent in the range needed by the action
    private bool inRange = false;
    // the cost of the action
    public float cost = 0f;
    // The target object to be acted
    public GameObject body;

    public Action(){
        preConditions = new HashSet<KeyValuePair<string, object>>();
        postConditions = new HashSet<KeyValuePair<string, object>>();
    }

    public void doReset(){
        inRange = false;
        body = null;
        reset();
    }

    public abstract void reset();

    public abstract bool isFinished();

    public abstract bool checkPreConditions(GameObject agent);

    public abstract bool perform(GameObject agent);

    public abstract bool requiresInRange();
    public bool isInRange(){
        return inRange;
    }
    public void setInRange(bool inRange){
        this.inRange = inRange;
    }

    public void addPreCondition(string key, object value){
        preConditions.Add(new KeyValuePair<string, object>(key, value));
    }

    public void removePreCondition(string key){
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach(KeyValuePair<string, object> pair in preConditions){
            if(pair.Key.Equals(key)){
                remove = pair;
            }
        }
        if(!default(KeyValuePair<string, object>).Equals(remove)){
            preConditions.Remove(remove);
        }
    }

    public void addPostCondition(string key, object value){
        postConditions.Add(new KeyValuePair<string, object>(key, value));
    }

    public void removePostCondition(string key){
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach(KeyValuePair<string, object> pair in postConditions){
            if(pair.Key.Equals(key)){
                remove = pair;
            }
        }
        if(!default(KeyValuePair<string, object>).Equals(remove)){
            postConditions.Remove(remove);
        }
    }

    public HashSet<KeyValuePair<string, object>> getPreConditions(){
        return preConditions;
    }

    public HashSet<KeyValuePair<string, object>> getPostConditions(){
        return postConditions;
    }
}
