using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.UI;
// using D;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 7; 
    // record W, A, S, D
    private float horizontal; 
    private float vertical; 
    private float gravity = 9.8f; 
    public float JumpSpeed = 5f;
    public CharacterController PlayerController; 
    private Vector3 Player_Move;

    public static bool isGhost = false;
    public Text text;

    public GameObject Nut;

    // Start is called before the first frame update
    void Start(){
        // Cursor.lockState = CursorLockMode.Locked;
        text.text = "Regular Mode";
    }

    // Update is called once per frame
    void Update()
    {
        // As same as the player controller in the assigment1
        // Check if the player is on the ground
        if(PlayerController.isGrounded) { 
            horizontal = Input.GetAxis("Horizontal"); 
            vertical = Input.GetAxis("Vertical");  
            Player_Move = (transform.forward * vertical + transform.right * horizontal) * MoveSpeed; 
            /*
            if (Input.GetAxis("Jump")==1) { 
                Player_Move.y = Player_Move.y + JumpSpeed;
            } 
            */
        }
        // Mimic the gravity so that the player will fall down from the sky
        Player_Move.y = Player_Move.y - gravity * Time.deltaTime;
        PlayerController.Move(Player_Move*Time.deltaTime);
        // To change the mode of the player
        if (Input.GetKeyDown("space")){
            if(isGhost){
                isGhost = false;
                text.text = "Regular Mode";
                // Debug.Log("Normal Mode");
            }
            else{
                isGhost = true;
                text.text = "Ghost Mode";
                // Debug.Log("Ghost Mode");
            }
        }
        Touch();
    }

    // Enable the player to change the status of the garbage can
    // Enable the player to destroy the nut object
    // Enable the player to create new nut
    void Touch(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetKeyDown(KeyCode.Mouse0) && isGhost){
            if(Physics.Raycast(ray, out RaycastHit hit, 10)){
                if(hit.transform.gameObject.tag == "Nut"){
                    GameObject nut = hit.transform.gameObject;
                    Destroy(nut);
                }
                if(hit.transform.gameObject.tag == "Can"){
                    TrashComponent can = hit.transform.gameObject.GetComponent<TrashComponent>();
                    if(can.IsEmpty()){
                        can.setStatus(false);
                    }
                    else{
                        can.setStatus(true);
                    }
                }
                if(hit.transform.gameObject.tag == "plane"){
                    Instantiate(Nut, hit.transform.gameObject.transform.position, Quaternion.identity);
                }
            }
        }
    }
}