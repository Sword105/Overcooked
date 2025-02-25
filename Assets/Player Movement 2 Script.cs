using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2Script : MonoBehaviour
{
    //controls characterController component - what controls the player
    public CharacterController controllerp1; //controllerp1 is the actual thing moving

    public float speed = 6f; //speed variable
    public float dash = 300f;
    public float turningSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Update()
    {
        //vector3 is a class - stores horizontal/vertical
        float horizontal = Input.GetAxisRaw("Horizontal"); // makes it so -1 and 1 for horizontal
        float vertical = Input.GetAxisRaw("Vertical"); // same thing as horizontal
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; //to move character on the x and z axis
                                                                               //normalize to make diagonal movement same speed

         //direction is the object of the class vector3
         //magnitude is the "length of direction vector" - 
    
        if(direction.magnitude >= 0.1f) {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //does math so angle of direction character is walking in is discovered

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turningSmoothTime); // a weird function that smoothens angles inside of unity
                                                                                                                                  // makes turning smooth instead of snappy

            transform.rotation = Quaternion.Euler(0f, angle, 0f); //takes variable of angle and applies to make character face direction they are walking in/has smoothness variable included


            controllerp1.Move(direction * speed * Time.deltaTime); //supposedly, by math standards solves the directional issue.

        }

        if(Input.GetKeyDown("e")){
            
            

            controllerp1.Move(direction * dash * Time.deltaTime); //dash potentially

            

        }




    }
}
