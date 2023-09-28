using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Gravity
    public float GravityPerSecond = 160.0f; //Falling Speed
    public float GroundLevel = 0.0f; //Ground Value

    //Movement
    public float MovementSpeedPerSecond = 10.0f; //Movement Speed

    void Update()
    {
        //Gravity 
        Debug.Log("Hej");
        Vector3 gravityPosition = transform.position; //Copy Character Pos
        gravityPosition.y -= GravityPerSecond * Time.deltaTime; //Subtract Gravity*Deltatime

        if(gravityPosition.y < GroundLevel) { gravityPosition.y = GroundLevel; } //Set Character To ground level

        transform.position = gravityPosition; //Assign New Pos to transform


        //Up
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 characterPosition = transform.position; //Copy Character Position
            characterPosition.y += MovementSpeedPerSecond * Time.deltaTime; //Add Movementspeed * Time for Frame
            transform.position = characterPosition; //Assign New position
        }

        //Down
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 characterPosition = transform.position;
            characterPosition.y -= MovementSpeedPerSecond * Time.deltaTime;
            transform.position = characterPosition;
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 characterPosition = transform.position;
            characterPosition.x -= MovementSpeedPerSecond * Time.deltaTime;
            transform.position = characterPosition;
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 characterPosition = transform.position;
            characterPosition.x += MovementSpeedPerSecond * Time.deltaTime;
            transform.position = characterPosition;
        }
    }
}
