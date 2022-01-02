using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner
{
    public Queue<Action> plan(GameObject agent, HashSet<Action> availableActions,
                                HashSet<KeyValuePair<string, object>> worldState,
                                HashSet<KeyValuePair<string, object>> goal){
        foreach(Action a in availableActions){
            a.doReset();
        }

        HashSet<Action> usableActions = new HashSet<Action>();
        foreach(Action a in availableActions){
            if(a.checkPreConditions(agent)){
                usableActions.Add(a);
            }
        }

        List<Node> leaves = new List<Node>();

        Node start = new Node(null, 0, worldState, null);
        bool success = buildGraph(start, leaves, usableActions, goal);

        if(!success){
            return null;
        }

        Node cheapest = leaves[Random.Range(0, 3)];
        foreach(Node leaf in leaves){
            // Debug.Log("Leaf Cost " + leaf.runningCost);
            // Debug.Log("Cheapest Cost " + cheapest.runningCost);
            if(helper(leaf) != null){
                if(leaf.runningCost < cheapest.runningCost){
                    cheapest = leaf;
                }
            }
        }
        return helper(cheapest);
    }

    // Helper function to retrieve all actions needed to achieve the goal
    private Queue<Action> helper(Node leaf){
        List<Action> result = new List<Action>();
        Node n = leaf;
        while(n != null){
            if(n.action != null){
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<Action> queue = new Queue<Action>();

        foreach(Action a in result){
            queue.Enqueue(a);
        }
        return queue;
    }

    // build the whole graph to find out if there is a possible way to achieve the goal
    private bool buildGraph(Node parent, List<Node> leaves, HashSet<Action> usableActions, 
                            HashSet<KeyValuePair<string, object>> goal){
        bool foundOne = false;
        foreach(Action action in usableActions){
            if(inState(action.getPreConditions(), parent.state)){
                HashSet<KeyValuePair<string, object>> currentState = populateState(parent.state, action.getPostConditions());
                Node node = new Node(parent, parent.runningCost + action.cost, currentState, action);
                if(inState(goal, currentState)){
                    leaves.Add(node);
                    foundOne = true;
                }
                else{
                    HashSet<Action> subset = actionSubset(usableActions, action);
                    bool found = buildGraph(node, leaves, subset, goal);
                    if(found){
                        foundOne = true;
                    }
                }
            }
        }
        return foundOne;
    }

    private HashSet<Action> actionSubset(HashSet<Action> actions, Action remove){
        HashSet<Action> subset = new HashSet<Action>();
        foreach(Action a in actions){
            if(!a.Equals(remove)){
                subset.Add(a);
            }
        }
        return subset;
    }

    private bool inState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> state){
        bool allMatch = true;
        foreach(KeyValuePair<string, object> t in test){
            bool match = false;
            foreach(KeyValuePair<string, object> s in state){
                if(s.Equals(t)){
                    match = true;
                    break;
                }
            }
            if(!match){
                allMatch = false;
            }
        }
        return allMatch;
    }

    private HashSet<KeyValuePair<string, object>> populateState(HashSet<KeyValuePair<string,object>> currentState, HashSet<KeyValuePair<string,object>> stateChange){
        HashSet<KeyValuePair<string,object>> state = new HashSet<KeyValuePair<string,object>> ();
		// copy the KVPs over as new objects
		foreach (KeyValuePair<string,object> s in currentState) {
			state.Add(new KeyValuePair<string, object>(s.Key,s.Value));
		}

		foreach (KeyValuePair<string,object> change in stateChange) {
			// if the key exists in the current state, update the Value
			bool exists = false;

			foreach (KeyValuePair<string,object> s in state) {
				if (s.Equals(change)) {
					exists = true;
					break;
				}
			}

			if (exists) {
				state.RemoveWhere( (KeyValuePair<string,object> kvp) => { return kvp.Key.Equals (change.Key); } );
				KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key,change.Value);
				state.Add(updated);
			}
			// if it does not exist in the current state, add it
			else {
				state.Add(new KeyValuePair<string, object>(change.Key,change.Value));
			}
		}
		return state;
    }

    // A private class used to build the whole tree structure

    private class Node {
		public Node parent;
		public float runningCost;
		public HashSet<KeyValuePair<string,object>> state;
		public Action action;

		public Node(Node parent, float runningCost, HashSet<KeyValuePair<string,object>> state, Action action) {
			this.parent = parent;
			this.runningCost = runningCost;
			this.state = state;
			this.action = action;
		}
	}
}
