using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public enum CharacterState // A enumerated list of character states
{
    Grounded = 0, //Is on the ground
    Airborne = 1, //Is in the air
    Jumping = 2, //IsCurrently Jumping In the air
    Total
}
public class CharacterController : MonoBehaviour
{
    public CharacterState JumpingState = CharacterState.Airborne; 
    //Is Our character on the ground or in the air?

    //Gravity
    public float GravityPerSecond = 160.0f; //Falling Speed
    public float GroundLevel = 0.0f; //Ground Value

    //Jump 
    public float JumpSpeedFactor = 3.0f; //How much faster is the jump than the movespeed?
    public float JumpMaxHeight = 150.0f;
    [SerializeField]
    private float JumpHeightDelta = 0.0f;
    //Movement
    public float MovementSpeedPerSecond = 10.0f; //Movement Speed



    void Update()
    {
     
        // Tells us if character is moving

        //if (transform.position.y <= GroundLevel)
        //{
        //    var characterpos = transform.position;
        //    characterpos.y = GroundLevel;
        //    transform.position = characterpos;
        //    GroundedState = CharacterState.Grounded;
        //}
        //else
        //{
        //    GroundedState = CharacterState.AirBorne;
        //}

        if(transform.position.y <= GroundLevel) //Check if Character is lower than or equal to ground
        {
            Vector3 characterPosition = transform.position;
            characterPosition.y = GroundLevel;
            transform.position = characterPosition;
            JumpingState = CharacterState.Grounded;
        }
        //Up
        if (Input.GetKey(KeyCode.W) && JumpingState == CharacterState.Grounded)
        {
            JumpingState = CharacterState.Jumping; //Set character to jumping
            JumpHeightDelta = 0.0f; //Restart Counting Jumpdistance
        }
        if(JumpingState == CharacterState.Jumping)
        {
            Vector3 characterPosition = transform.position;
            float totalJumpMovementThisFrame = MovementSpeedPerSecond* JumpSpeedFactor *Time.deltaTime;
            characterPosition.y += totalJumpMovementThisFrame;
            transform.position = characterPosition;
            JumpHeightDelta += totalJumpMovementThisFrame;
            if(JumpHeightDelta >= JumpMaxHeight)
            {
                JumpingState = CharacterState.Airborne;
                JumpHeightDelta = 0.0f;
            }
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



        if (JumpingState == CharacterState.Airborne ) // Checks if isMoving is false and adds gravity if that is true
        {
            Vector3 gravityPosition = transform.position; //Copy Character Pos
            gravityPosition.y -= GravityPerSecond * Time.deltaTime; //Subtract Gravity*Deltatime

            //Set Character To ground level
            if (gravityPosition.y < GroundLevel) { gravityPosition.y = GroundLevel; }
            transform.position = gravityPosition; //Assign New Pos to transform
        }
    }
}


//Vector3 characterPosition = transform.position; //Copy Character Position
//characterPosition.y += MovementSpeedPerSecond * Time.deltaTime; //Add Movementspeed * Time for Frame
//transform.position = characterPosition; //Assign New position