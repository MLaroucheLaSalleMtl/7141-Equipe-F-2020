﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   
    [SerializeField] private float MovementSpeed = 15.0f;
    [SerializeField] private float JumpPower = 5.0f;
    [SerializeField] private float SuperJumpPower = 15.0f;
    [SerializeField] private float SprintSpeed = 30.0f;
    private float CurrentSpeed;
    public CharacterController Controller;
    Vector3 Velocity;
    private float Gravity = 9.807f;
    [SerializeField]private Transform GroundCheck;
    private float GroundDistance = 0.2f;
    public LayerMask GroundMask;
    public bool IsGrounded;
    public bool IsSprinting = false;
    private void Jump()
    {
        Velocity.y = Mathf.Sqrt(JumpPower * 2f * Gravity);
    }
    private void SuperJump()
    {
        Velocity.y = Mathf.Sqrt(SuperJumpPower * 2f * Gravity);
    }
    private void Check()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
        if (IsGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

    }
    private void Movement()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        Controller.Move(move * CurrentSpeed * Time.deltaTime);
    }
    private void Sprint()
    {
       

    }
    private void Update()
    {
        Check();

        Movement();

        if (Input.GetButton("Sprint") && IsGrounded==true && IsSprinting==false)
        {
            CurrentSpeed = SprintSpeed;
            IsSprinting = true;
 
        }
        else
        {
            CurrentSpeed = MovementSpeed;
            IsSprinting = false;
        }
        
       

        if (Input.GetButtonDown("Jump") && IsGrounded==true && IsSprinting==false)
        {
            Jump(); 
        }
        if (Input.GetButtonDown("Jump") && IsGrounded == true && IsSprinting == true)
        {
            SuperJump();
        }

        Velocity.y += -Gravity * Time.deltaTime;

        Controller.Move(Velocity * Time.deltaTime); 
    }

}