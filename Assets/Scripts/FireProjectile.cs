using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initially, I want to use this one to do the ghost mode. Unfortunately, it does not work at all
public class FireProjectile : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 2000;
    public Camera mainCamera;
    private bool canFire = true;
    private Rigidbody bullet;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canFire){
            bullet = Instantiate(projectile, mainCamera.transform.position, Quaternion.identity);
            bullet.AddForce(mainCamera.transform.forward * speed);
            // canFire = false;
        }
        // Can only fire one bullet at one time
        if(bullet == null){
            canFire = true;
        }
    }

}
