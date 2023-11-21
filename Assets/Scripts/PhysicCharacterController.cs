using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PhysicsCharacterController : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer = null;
    public List<Sprite> Charactersprites = new List<Sprite>();
    //Player Health
    public int HP = 0;

    //Refrence to rigidbody on same object
    public Rigidbody2D myRigidBody = null;

    public CharacterState JumpingState = CharacterState.Airborne;
    //Is Our character on the ground or in the air?

    //Gravity
    public float GravityPerSecond = 160.0f; //Falling Speed
    public float GroundLevel = 0.0f; //Ground Value

    //Jump 
    public float JumpSpeedFactor = 3.0f; //How much faster is the jump than the movespeed?
    public float JumpMaxHeight = 150.0f;
    //How far have we flew this jump?
    public float JumpHeightDelta = 0.0f;
   
    //Movement
    public float MovementSpeedPerSecond = 10.0f; //Movement Speed

  

    private void Update()
    {
        //Check if HP is lesser than Zero
        if(HP <= 0)
        {
            //Try To get a reference to SceneloaderScript on this gameobject
            Sceneloader mySceneLoader = gameObject.GetComponent<Sceneloader>();
            //Check if We Succeeded?
            if(mySceneLoader != null)
            {
                //Load the gameover-scene
                mySceneLoader.LoadScene("Game Over");
            }
        }


        //Copy our HP-1 to new Variable
        int hpCopy = HP-1;
        //If hpCopy is lesser(<) than Zero(0), set it to Zero(0)    
        if (hpCopy < 0)
        {
            hpCopy = 0;
        }
        //If hpCopy is larger or equal to the number of different
        //Sprites in Charactersprites, set it to that number minus one(-1)
        if (hpCopy >= Charactersprites.Count)
        {
            hpCopy = Charactersprites.Count - 1;
        }
        //Assign Correct Sprite to renderer component
        mySpriteRenderer.sprite = Charactersprites[hpCopy];


        if (Input.GetKeyDown(KeyCode.W) && JumpingState == CharacterState.Grounded)
        {
            JumpingState = CharacterState.Jumping; //Set character to jumping
            JumpHeightDelta = 0.0f; //Restart Counting Jumpdistance
        }
    }

    void FixedUpdate()
    {
        //Copy Velocity
        Vector3 characterVelocity = myRigidBody.velocity;
        characterVelocity.x = 0.0f;
        
        //Jump
        if (JumpingState == CharacterState.Jumping)
        {
            float jumpMovement = MovementSpeedPerSecond * JumpSpeedFactor;
            characterVelocity.y = jumpMovement;

            JumpHeightDelta += jumpMovement*Time.deltaTime;

            if (JumpHeightDelta >= JumpMaxHeight)
            {
                JumpingState = CharacterState.Airborne;
               
            }
        }

        //Left
        if (Input.GetKey(KeyCode.A))
        {
            characterVelocity.x -= MovementSpeedPerSecond;
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            characterVelocity.x += MovementSpeedPerSecond;
        }
        myRigidBody.velocity = characterVelocity;

    }

    public void TakeDamage(int aHPValue)
    {
        HP += aHPValue;
    }
}

