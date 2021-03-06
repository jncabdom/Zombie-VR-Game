﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Apuntes para la configuración del characterController
// * Hay que añadir un character controller al objeto y configurar los siguientes parámetros
// * Step offset permite controlar el tamaño de los escalones si es menor que el que tiene sube las escaleras
// * Slope Limit controla el ángulo máximo que sube el objeto
// * Hay que añadir un objeto GroundChecker en la parte inferior para controlar si está en contacto con el suelo,
//   detectará colisiones con todos los objetos que estén en el Layer Ground

public class PlayerController : MonoBehaviour
{ 
  public Transform cameraTransform;

  // Player Movement
  public float moveSpeed = 12f;
  public float crouchSpeed = 6f;
  private CharacterController controller;

  // Gravity
  public float gravity = -9.81f;
  public float velocityY = 0f;
  public float minVelocityY = -2f;

  // GroundChecking
  bool isGrounded;
  public float groundDistance = 0.4f;
  public Transform groundCheck;
  public LayerMask groundMask;

  // Jump
  public float jumpHeight = 3f;

  // Crouch
  private bool crouch = false;
  public Transform playerCamera;
  public float timeToReachObjective = 5f;
  private float crouchingYPosition = 0f;
  private float regularYPosition = 1.5f;

  // Gets the CharacterController from the GameObject
  private void Start() {
    controller = gameObject.GetComponent<CharacterController>();
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Here check if movement is required 
  void Update() {
    Move();
    CalculateGravity();
    Jump();
    Crouch();
   }

  // Get the movement in the x and z axis then we add the speed and the direction of our gameObject
  private void Move() {
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    Vector3 move = cameraTransform.right * x + cameraTransform.forward * z;
    controller.Move(move * moveSpeed * Time.deltaTime);
  }

  // If we are grounded it resets the velocity, otherwise we add the gravity force to the velocity causing it to move down
  private void CalculateGravity() {
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocityY < 0)
      velocityY = minVelocityY;
    else {
      velocityY += gravity * Time.deltaTime;
      controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);
    }
  }

  // Check if we can jump then if the button is pressed we change the velocity in the Y axis
  private void Jump() {
    if (Input.GetButtonDown("Jump") && isGrounded)
      velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
  }

  // If crouch button is pressed and the player is in the ground then it lowers the camera to crouchingYPosition
  private void Crouch() {
    if (Input.GetButtonDown("Crouch") && isGrounded)
      crouch = !crouch;
    if (crouch) {
      moveSpeed = crouchSpeed;
      Vector3 crouchingPosition = new Vector3(transform.position.x, transform.position.y + crouchingYPosition, transform.position.z);
      playerCamera.position = Vector3.MoveTowards(playerCamera.position, crouchingPosition, timeToReachObjective * Time.deltaTime);
    } else {
      moveSpeed = crouchSpeed * 2f;
      Vector3 regularPosition = new Vector3(transform.position.x, transform.position.y + regularYPosition, transform.position.z);
      playerCamera.position = Vector3.MoveTowards(playerCamera.position, regularPosition, timeToReachObjective * Time.deltaTime);
    }
  }
}