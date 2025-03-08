using UnityEngine.InputSystem;
using UnityEngine;

public class MovementScript3 : MonoBehaviour
{
    
    public Rigidbody player;
    public float speed = 10f;
    float weirdFloat;

    private Vector3 movementInput = Vector3.zero;


    public void OnMove(InputAction.CallbackContext context) {
        movementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);

    }

   
    void FixedUpdate() {

        player.MovePosition((Vector3)transform.position + movementInput * speed * Time.deltaTime); // movementposition used for movement, vector3 for 3d

        //makes the magnitude thing so that if you are clicking on a button or not - keeps character looking in the same direction
        if(movementInput.magnitude >=0.1f){

        float angle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg; //certain math thing that apparently almost all games use,
                                                                            //to make character face certain direction

        float smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref weirdFloat, 0.1f); //makes a smooth rotation variable for character to turn smoothly
                                                                                                    //has a weird float variable that is needed - not known why

        transform.rotation = Quaternion.Euler(0, smooth, 0); // used the smooth variable that was retrieved to make the rotation of character face the direction smoothly

        

        }

    

    }




}

