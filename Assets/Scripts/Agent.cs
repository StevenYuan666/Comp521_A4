using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class Agent : MonoBehaviour
{
    private FiniteStateMachine stateMachine;

    private FiniteStateMachine.State idleState;
    private FiniteStateMachine.State moveToState;
    private FiniteStateMachine.State performActionState;

    private HashSet<Action> availableActions;
    private Queue<Action> currentActions;

    private GOAPInterface dataProvider;

    private Planner planner;

    private float startTime;

    public GameObject character;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("Initialize", 0f);
    }

    void Initialize(){
        startTime = Time.time;
        stateMachine = new FiniteStateMachine();
        availableActions = new HashSet<Action>();
        currentActions = new Queue<Action>();
        planner = new Planner();
        findDataProvider();
        createIdleState();
        createMoveToState();
        createPerformActionState();
        stateMachine.push(idleState);
        loadActions();
    }

    // Update is called once per frame
    void Update()
    {
        // If the character is not too close, do the normal actions
        if(Time.time - startTime > 0.1f){
            if((Vector3.Distance(this.gameObject.transform.position, character.transform.position) >= 3f) || PlayerMove.isGhost){
                stateMachine.Update(gameObject);
            }
            else{
                // if the character is too close, seeking refuge in the nearest tree
                SeekingRefuge();
            }
        }
    }

    // Find the nearest tree and move toward it
    void SeekingRefuge(){
        TreeComponent[] trees = (TreeComponent[]) UnityEngine.GameObject.FindObjectsOfType(typeof(TreeComponent));
        TreeComponent closest = null;
        float dist = 0f;

        foreach(TreeComponent t in trees){
            if(closest == null){
                closest = t;
                dist = Vector3.Distance(t.gameObject.transform.position, this.gameObject.transform.position);
            }
            else{
                float temp = Vector3.Distance(t.gameObject.transform.position, this.gameObject.transform.position);
                if(temp < dist){
                    closest = t;
                    dist = temp;
                }
            }
        }
        if(closest == null){
            return;
        }
        float moveSpeed = 10f;
        float step = moveSpeed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, closest.transform.position + new Vector3(0.3f, 0f, 0), step);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, closest.transform.position + new Vector3(0.3f, 1f, 0), step);
        // gameObject.transform.position = closest.transform.position;
    }

    public void addAction(Action action){
        availableActions.Add(action);
    }

    public Action getAction(Type action){
        foreach(Action a in availableActions){
            if(a.GetType().Equals(action)){
                return a;
            }
        }
        return null;
    }

    public void removeAction(Action action){
        availableActions.Remove(action);
    }

    private bool hasActionPlan(){
        return currentActions.Count > 0;
    }

    // Create the initial idle state
    private void createIdleState(){
        idleState = (fsm, obj) => {
            HashSet<KeyValuePair<string, object>> worldState = dataProvider.getWorld();
            HashSet<KeyValuePair<string, object>> goal = dataProvider.createGoal();

            //Plan
            Queue<Action> plan = planner.plan(gameObject, availableActions, worldState, goal);
            if(plan != null){
                currentActions = plan;
                dataProvider.planFound(goal, plan);

                fsm.pop();
                fsm.push(performActionState);
            }
            else{
                dataProvider.planFailed(goal);
                fsm.pop();
                fsm.push(idleState);
            }
        };
    }

    // Create the state need the agent to move
    private void createMoveToState(){
        // Debug.Log("Create Move");
        moveToState = (fsm, obj) => {
            Action action = currentActions.Peek();

            if(action.requiresInRange() && action.body == null){
                // Debug.Log("Not moving...");
                fsm.pop();
                fsm.pop();
                fsm.push(idleState);
                return;
            }

            if(dataProvider.move(action)){
                // Debug.Log("Moving...");
                fsm.pop();
            }
        };
    }

    // Create the state need to perform an action
    private void createPerformActionState(){
        performActionState = (fsm, obj) => {
            if(!hasActionPlan()){
                fsm.pop();
                fsm.push(idleState);
                dataProvider.actionsFinished();
                return;
            }

            Action action = currentActions.Peek();
            if(action.isFinished()){
                currentActions.Dequeue();
            }
            if(hasActionPlan()){
                action = currentActions.Peek();
                // Debug.Log("If requiresInRange" + action.requiresInRange());
                bool inRange;
                if(action.requiresInRange()){
                    inRange = action.isInRange();
                }
                else{
                    inRange = true;
                }
                // Debug.Log("If in range" + inRange);
                if(inRange){
                    // fsm.push(moveToState);
                    bool success = action.perform(obj);

                    if(!success){ 
                        fsm.pop();
                        fsm.push(idleState);
                        dataProvider.planAborted(action);
                    }
                }
                else{
                    fsm.push(moveToState);
                }
            }
            else{
                fsm.pop();
                fsm.push(idleState);
                dataProvider.actionsFinished();
            }
        };
    }

    // Load the specific implementation of the GOAP interface
    private void findDataProvider(){
        foreach(Component comp in gameObject.GetComponents(typeof(Component))){
            if(typeof(GOAPInterface).IsAssignableFrom(comp.GetType())){
                dataProvider = (GOAPInterface) comp;
                return;
            }
        }
    }

    // Find all actions that the squirrel can do
    private void loadActions(){
        Action[] actions = gameObject.GetComponents<Action>();
        foreach(Action a in actions){
            availableActions.Add(a);
        }
    }

}
