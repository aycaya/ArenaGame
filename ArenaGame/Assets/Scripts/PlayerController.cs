//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//     CharacterController controller;
//     Vector3 playerVelocity;
//     bool groundedPlayer;
//    [SerializeField]
//     float playerSpeed = 2.0f;

//    [SerializeField]
//    float jumpHeight = 1.0f;

//    [SerializeField]
//    float gravityValue = -9.81f;
//    PlayerControlls playerInput;
//    Vector3 gravityForce;
//    Vector3 currentGravityForce = Vector3.zero;
//    private void Awake()
//    {
//        playerInput = new PlayerControlls();
//        controller = GetComponent<CharacterController>();
//    }
//    private void OnEnable()
//    {
//        playerInput.Enable();
//    }
//    private void OnDisable()
//    {
//        playerInput.Disable();
//    }
//    private void Start()
//    {

//    }

//    void Update()
//    {
//        ControlCharacter();
//        HandleGravity();
//    }
//    private void ControlCharacter()
//    {

//        groundedPlayer = controller.isGrounded;
//        if (groundedPlayer && playerVelocity.y < 0)
//        {
//            playerVelocity.y = 0f;
//        }
//        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
//        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
//        controller.Move(move * Time.deltaTime * playerSpeed);

//        if (move != Vector3.zero)
//        {
//            gameObject.transform.forward = move;
//        }

//        // Changes the height position of the player..
//        //if (groundedPlayer)
//        //{
//        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
//        //}

//        //playerVelocity.y += gravityValue * Time.deltaTime;
//        //controller.Move(playerVelocity * Time.deltaTime);

//    }
//        private void HandleGravity()
//    {
//        if (!controller.isGrounded)
//        {
//            currentGravityForce += gravityForce * Time.deltaTime;
//            controller.Move(currentGravityForce * Time.deltaTime);
//        }
//        else
//        {
//            currentGravityForce = Vector3.zero;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool disablePlayerControl = false;
    CharacterController charController;
   // [SerializeField] InputAction joystick;
    [SerializeField] float speed = 1f;
    [SerializeField] float turnSmoothCoef = 0.1f;
    float speedIncreaseCoefficient = 0.3f;
    public int SpeedUpgrade { get; set; }
    private float turnSmoothVelocity;
    public float vertical = 0f;
    public float horizontal = 0f;
    Vector3 direction;
    Vector3 gravityForce;
    Vector3 currentGravityForce = Vector3.zero;
    [SerializeField] float ABTestExtraSpeed = 1.5f;
    PlayerControlls playerInput;
    public float PlayerSpeed
    {
        get
        {
            return direction.magnitude;
        }
    }


    private void Awake()
    {
        //if (SceneManager.GetActiveScene().buildIndex != 3)
        //{
        //    ABTestExtraSpeed = 0f;
        //}
        playerInput = new PlayerControlls();
        gravityForce = Physics.gravity;
        charController = GetComponent<CharacterController>();
        SpeedUpgrade = PlayerPrefs.GetInt("Speed", 0);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        ControlCharacter();
        HandleGravity();
    }

    private void ControlCharacter()
    {
        horizontal = playerInput.Player.Move.ReadValue<Vector2>().x;
        vertical = playerInput.Player.Move.ReadValue<Vector2>().y;
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (disablePlayerControl)
        {
            horizontal = 0f;
            vertical = 0f;
            direction = Vector3.zero;
        }
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg) + 20f;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothCoef);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            charController.Move(moveDir.normalized * (ABTestExtraSpeed + speed + (SpeedUpgrade * speedIncreaseCoefficient)) * Time.deltaTime);
        }
    }
    public void UpdateSpeed()
    {
        SpeedUpgrade = PlayerPrefs.GetInt("Speed", 0);

    }
    private void HandleGravity()
    {
        if (!charController.isGrounded)
        {
            currentGravityForce += gravityForce * Time.deltaTime;
            charController.Move(currentGravityForce * Time.deltaTime);
        }
        else
        {
            currentGravityForce = Vector3.zero;
        }
    }
}
