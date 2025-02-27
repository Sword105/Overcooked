using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript3 : MonoBehaviour
{
    
    public Rigidbody player;
    public Vector3 InputKey;
    public float speed = 10f;
    float weirdFloat;



    void Update() {
        
    InputKey = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; //makes -1 and 1 for movement signaling

    }

   
    void FixedUpdate() {

        player.MovePosition((Vector3)transform.position + InputKey * speed * Time.deltaTime); // movementposition used for movement, vector3 for 3d

        //makes the magnitude thing so that if you are clicking on a button or not - keeps character looking in the same direction
        if(InputKey.magnitude >=0.1f){

        float angle = Mathf.Atan2(InputKey.x, InputKey.z) * Mathf.Rad2Deg; //certain math thing that apparently almost all games use,
                                                                            //to make character face certain direction

        float smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref weirdFloat, 0.1f); //makes a smooth rotation variable for character to turn smoothly
                                                                                                    //has a weird float variable that is needed - not known why

        transform.rotation = Quaternion.Euler(0, smooth, 0); // used the smooth variable that was retrieved to make the rotation of character face the direction smoothly

        

        }

    

    }


}

