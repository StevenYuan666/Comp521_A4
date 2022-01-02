using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The finite state machine to record the state of a squirrel
public class FiniteStateMachine
{
    private Stack<State> stateStack = new Stack<State>();
    public delegate void State(FiniteStateMachine fsm, GameObject gameObject);

    // Update by doing the first action
    public void Update(GameObject gameObject){
        if(stateStack.Peek() != null){
            stateStack.Peek().Invoke(this, gameObject);
        }
    }

    public void push(State state){
        stateStack.Push(state);
    }

    public void pop(){
        stateStack.Pop();
    }
}
