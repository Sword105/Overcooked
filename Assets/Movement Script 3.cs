using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class MovementScript3 : MonoBehaviour
{
    public Rigidbody player;
    public float speed = 10f;
    float weirdFloat;    

    [Header("Dashing")]
    public bool dashing = false;

    public int dashCounter = 5;

    private int currentDashes;
    private float dashingPower = 5f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;
    private bool isOnCoolDown = false;
    private float coolDownDashing = 0.75f;

    private Vector3 movementInput;
    private bool dashed = false;

    public LayerMask raycastIgnoreLayer;

    public void OnMove(InputAction.CallbackContext context) {
        if (!dashing)
        {
            movementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }

    /*
    public void OnDash(InputAction.CallbackContext context){
        if(context.performed){
            dashed = true;
        }
    }
    */

    void Start(){

        currentDashes = dashCounter;
        player = GetComponent<Rigidbody>();
    }


    void Update() {

       if(dashed && !isOnCoolDown) {

        StartCoroutine(Dash());
        

       }

       dashed = false; 

 }

   
    void FixedUpdate() {

        /*
        if (!Physics.Raycast(transform.position, transform.forward, 0.8f, raycastIgnoreLayer))
        {
            player.MovePosition((Vector3)transform.position + movementInput * speed * Time.deltaTime); // movementposition used for movement, vector3 for 3d
        }
        */

        player.MovePosition((Vector3)transform.position + movementInput * speed * Time.deltaTime);


        //makes the magnitude thing so that if you are clicking on a button or not - keeps character looking in the same direction
        if (movementInput.magnitude >=0.1f){

        float angle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg; //certain math thing that apparently almost all games use,
                                                                            //to make character face certain direction

        float smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref weirdFloat, 0.05f); //makes a smooth rotation variable for character to turn smoothly
                                                                                                    //has a weird float variable that is needed - not known why

        transform.rotation = Quaternion.Euler(0, smooth, 0); // used the smooth variable that was retrieved to make the rotation of character face the direction smoothly
        }
    }

    private IEnumerator Dash() {
        dashing = true;
        isOnCoolDown = true;
        //currentDashes--;


        movementInput = new Vector3(transform.forward.x * dashingPower, 0f, transform.forward.z * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        movementInput = Vector3.zero;
        dashing = false;

        yield return new WaitForSeconds(coolDownDashing);
        isOnCoolDown = false;
        
    }

}