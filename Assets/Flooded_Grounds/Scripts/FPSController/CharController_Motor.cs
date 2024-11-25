using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController_Motor : MonoBehaviour
{
	public float speed = 10.0f;
	public float sensitivity = 30.0f;
	public float gravity = -9.81f;
	CharacterController character;
	public GameObject cam;
	public GameObject gun; // Referencia al arma
	float moveFB, moveLR;
	float rotX, rotY;
	public bool webGLRightClickRotation = true;
	private Vector3 velocity;

	private Animator animator;

	void Start()
	{
		character = GetComponent<CharacterController>();
		if (Application.isEditor)
		{
			webGLRightClickRotation = false;
			sensitivity = sensitivity * 1.5f;
		}
		animator = GetComponent<Animator>();
		Cursor.lockState = CursorLockMode.Locked;

		// Asegurar que el personaje inicie en el suelo
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit))
		{
			transform.position = hit.point;
		}
	}

	void Update()
	{
		// Movimiento horizontal y vertical
		moveFB = Input.GetAxis("Horizontal") * speed;
		moveLR = Input.GetAxis("Vertical") * speed;

		// Rotación de cámara
		rotX = Input.GetAxis("Mouse X") * sensitivity;
		rotY = Input.GetAxis("Mouse Y") * sensitivity;

		// Actualizar animador
		animator.SetFloat("PosX", Input.GetAxis("Horizontal"));
		animator.SetFloat("PosY", Input.GetAxis("Vertical"));

		// Crear vector de movimiento
		Vector3 movement = new Vector3(moveFB, 0, moveLR);

		// Aplicar gravedad
		if (character.isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}
		velocity.y += gravity * Time.deltaTime;

		// Rotar cámara y arma
		CameraRotation(cam, gun, rotX, rotY);

		// Aplicar movimiento relativo a la rotación
		movement = transform.rotation * movement;
		character.Move(movement * Time.deltaTime);

		// Aplicar gravedad
		character.Move(velocity * Time.deltaTime);
	}

	void CameraRotation(GameObject cam, GameObject gun, float rotX, float rotY)
	{
		transform.Rotate(0, rotX * Time.deltaTime, 0);

		// Limitar rotación vertical de la cámara
		float currentRotation = cam.transform.localEulerAngles.x;
		float newRotation = currentRotation - rotY * Time.deltaTime;

		if (newRotation > 180)
			newRotation -= 360;

		newRotation = Mathf.Clamp(newRotation, -80, 80);
		cam.transform.localEulerAngles = new Vector3(newRotation, 0, 0);

		// Rotar el arma junto con la cámara
		if (gun != null)
		{
			gun.transform.localEulerAngles = cam.transform.localEulerAngles;
		}
	}
}
