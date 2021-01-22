using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("GameObjects && Components")]
    public GameObject cam;
    public Vector3 playerVelocity;
    CharacterController controller;
    [Header("Physics")]
    [Range(0.0f, 100.0f)]
    public float MovementSpeed = 1;
    [Range(0.0f, -100.0f)]
    public float gravity = -9.81f;
    [Range(0.0f, 100.0f)]
    public float velocity = 0;
    [Range(0.0f, 100.0f)]
    public float rotSpeed = 100f;
    [Range(0.0f, 100.0f)]
    public float jumpHeight = 1f;
    public bool grounded;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        GameManager.Instance.playerIsGrounded = grounded;
        grounded = CheckGrounded();
        Movement();

        if(!grounded)
        playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }
    bool CheckGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0.5f,0,0), Vector3.down, 1.5f, 1 << LayerMask.NameToLayer("Ground")) || Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), Vector3.down, 1.5f, 1 << LayerMask.NameToLayer("Ground")) || Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), Vector3.down, 1.5f, 1 << LayerMask.NameToLayer("Ground")) || Physics.Raycast(transform.position + new Vector3(0, 0, -0.5f), Vector3.down, 1.5f, 1 << LayerMask.NameToLayer("Ground"));

    }
    void Movement()
    {
        Quaternion target = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotSpeed);

        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        controller.Move((transform.right * horizontal + transform.forward * vertical) * Time.deltaTime);
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            
        }
    }

}
