using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonyRun : MonoBehaviour
{
    //Component variables attached to components that need to be editied within the script.
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Camera playerCamera;
    //Numbered variables that determine the parameters of the character controller.
    public float gravity = -10f;
    public float speed = 25f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 2f;
    bool isGrounded;
    Vector3 velocity;
    //Numbered variables that are used to determine the camera bob.
    public float walkBobSpeed = 14f;
    public float walkBobAmount = 0.05f;
    private float defaultYPos = 0;
    private float timer;

    //Awake is called the first time the script is run.
    private void Awake()
    {
        defaultYPos = playerCamera.transform.localPosition.y;
    }

    // Update is called once per frame.
    void Update()
    {
        //Returns true if the imaginary sphere under the player is touching something other than the player.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //Resets velocity if player is on the ground and has velocity.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //These 2 lines get the input from the keyboard for forward & backwards and left & right.
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        /*Originally I had the movement with the format of new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))
         * but that only works when you aren't moving the player based on where it's facing. This format makes it that forward is always
         * forward from where the camera is facing.*/
        Vector3 movement = transform.right * x + transform.forward * z;
        //Moves the player.
        controller.Move(speed * Time.deltaTime * movement);
        //Constantly applies the value of gravity every frame. The second Time.deltaTime is in accordance with the formula for a free fall.
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Uses a formula to calculate the jump if the player is on the ground and presses the space key.
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //If the player is on the ground and the player moves left, right, up or down, add a slight bob to the camera.
        if (isGrounded && (Mathf.Abs(movement.x) > 0.1f || Mathf.Abs(movement.z) > 0.1f))
        {
            timer += Time.deltaTime * walkBobSpeed;
            playerCamera.transform.localPosition = new Vector3( playerCamera.transform.localPosition.x, defaultYPos + Mathf.Sin(timer) * walkBobAmount, playerCamera.transform.localPosition.z);
        }
    }
}
