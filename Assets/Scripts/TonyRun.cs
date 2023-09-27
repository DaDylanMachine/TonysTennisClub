using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TonyRun : MonoBehaviour
{
    //Component variables attached to components that need to be editied within the script.
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Camera playerCamera;
    public Slider barOfStamina;

    //Numbered variables that determine the parameters of the character controller.
    public float gravity = -100f;
    public float walkSpeed = 16f;
    public float sprintSpeed = 32f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 2f;
    public float maxWalkSpeed = 20f;
    public float maxSprintSpeed = 37f;
    private bool isGrounded;
    private bool isSprinting;
    private bool outOfStamina = false;
    Vector3 velocity;
    Vector3 movement;

    //Numbered variables that are used to determine the camera bob.
    public float walkBobSpeed = 8.5f;
    public float walkBobAmount = 0.15f;
    public float sprintBobSpeed = 17f;
    public float sprintBobAmount = 0.25f;
    private float defaultYPos = 0;
    private float timer;

    //Awake is called the first time the script is run.
    private void Awake()
    {
        defaultYPos = playerCamera.transform.localPosition.y;
    }

    // Update is called once per frame.
    public void Update()
    {
        //Returns true if the imaginary sphere under the player is touching something other than the player.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Pulls the staminaDepleted variable from the stamina script to use here.
        outOfStamina = barOfStamina.GetComponent<StaminaBar>().staminaDepleted;

        //Resets velocity if player is on the ground and has velocity.
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        //Checks if the player is sprinting by holding down left shift.
        if (Input.GetKey(KeyCode.LeftShift) && outOfStamina == false)
            isSprinting = true;
        else
            isSprinting = false;

        //These 2 lines get the input from the keyboard for forward & backwards and left & right.
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        //Calculates the movement based on the input from the player. 
        movement = transform.right * x + transform.forward * z;
        //Keeps speed inline when moving diagonally.
        if (x != 0 && z != 0)
            movement = movement * .75f;
        
        //Constantly applies the value of gravity every frame. The second Time.deltaTime is in accordance with the formula for a free fall.
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Uses a formula to calculate the jump if the player is on the ground and presses the space key.
        if (isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        //If the player is on the ground and the player moves left, right, up or down, add a slight bob to the camera.
        if (isGrounded && (Mathf.Abs(movement.x) > 0.1f || Mathf.Abs(movement.z) > 0.1f))
        {

            timer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x, 
                defaultYPos + Mathf.Sin(timer) * (isSprinting ? sprintBobAmount : walkBobAmount), 
                playerCamera.transform.localPosition.z);
        }

    }

    //
    private void FixedUpdate()
    {
        //Moves the player at the walking or sprinting speed depending on if the variable is true or false.
        if (isSprinting)
        {
            controller.Move(sprintSpeed * Time.deltaTime * movement);
            //Deplete the stamina bar while the user is sprinting.
            StaminaBar.instance.UseStamina(1);
        }
        else
            controller.Move(walkSpeed * Time.deltaTime * movement);
    }
}
