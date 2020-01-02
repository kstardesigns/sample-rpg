using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    //public allows it to show up in Unity in the Inspector
    public float movementSpeed = 3.0f;

    //holds 2D vectors or points, stores location
    Vector2 movement = new Vector2();

    //variable to store reference to animator component in the GameObject where this script is attached
    Animator animator;

    //hardcoded string value to set its integer later on
    string animationState = "AnimationState";


    //declare variable to reference the Rigidbody2D
    Rigidbody2D rb2D;

    //set of enumerated constants
    enum CharStates {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idleSouth = 5
    }

    // Start is called before the first frame update
    private void Start() {

        //grab a reference to the Animator component in the GameObject to which this script is attached
        animator = GetComponent<Animator>();

        //GetComponent taking parameter of type (Rigidbody2D, which references the component we attached to PlayerObject in Unity)
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        UpdateState();
    }

    //called at fixed intervals by Unity Engine
    void FixedUpdate() {
        MoveCharacter();
    }

    private void MoveCharacter() {
        //assigning to Vector2 structure above - Horiz and Vert are parameters of GetAxisRaw. 
        movement.x = Input.GetAxisRaw("Horizontal"); //-1 = left/a, 0 = no key, 1 = right/d (using wasd and arrows)
        movement.y = Input.GetAxisRaw("Vertical");
        //keeps player moving at same rate of speed whether diagonal, vert, or horiz
        movement.Normalize();
        //set veloctiy of rigidbody
        rb2D.velocity = movement * movementSpeed;
    }

    private void UpdateState() {
        //these set the animator integer to -1, 0, or 1 based on MoveCharacter above, which triggers the animation states
        if (movement.x > 0) {
            animator.SetInteger(animationState, (int)CharStates.walkEast);
        }
        else if (movement.x < 0) {
            animator.SetInteger(animationState, (int)CharStates.walkWest);
        }
        else if (movement.y > 0) {
            animator.SetInteger(animationState, (int)CharStates.walkNorth);
        }
        else if (movement.y < 0) {
            animator.SetInteger(animationState, (int)CharStates.walkSouth);
        } else {
            animator.SetInteger(animationState, (int)CharStates.idleSouth);
        }
    }
}
