using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float dashSpeed;

    private bool Moving;
    [SerializeField] private bool isJump;

    Animator anim;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Fall();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Moving = moveInput.magnitude != 0;
        anim.SetBool("isMove", Moving);
        if (Moving)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            transform.forward = moveDir;
            Vector3 des = transform.position + moveDir * moveSpeed * Time.deltaTime;
            rigid.MovePosition(des);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            anim.SetTrigger("isJump");
            Debug.Log("Jump!");
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            isJump = true;
        }
    }

    private void Fall()
    {
        if (isJump)
        {
            rigid.AddForce(Vector3.down * Time.deltaTime * gravity, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }
}
