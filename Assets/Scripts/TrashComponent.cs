using UnityEngine;
using System.Collections;

// To control the garbage can
public class TrashComponent : MonoBehaviour{
    bool isEmpty;
    float CanTimer = 10f;

    void Start(){
        if(Random.Range(0, 2) == 0){
            isEmpty = true;
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else{
            isEmpty = false;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Update the status of the garbage can every ten seconds
    void Update(){
        CanTimer -= Time.deltaTime;
        if (CanTimer <= 0)
        {
            UpdateCans();
            CanTimer = 10f;
        }
        if(isEmpty == true){
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else{
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public bool IsEmpty(){
        return isEmpty;
    }

    void UpdateCans(){
        if(Random.Range(0, 2) == 0){
            isEmpty = true;
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else{
            isEmpty = false;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void setStatus(bool status){
        this.isEmpty = status;
    }
}