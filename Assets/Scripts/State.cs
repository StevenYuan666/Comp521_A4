using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The interface to represent a state
public interface State{
    void Update(FiniteStateMachine fsm, GameObject gameObject);
}
