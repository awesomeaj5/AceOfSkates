using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skater : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;
    Animator myAnimator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCapsuleCollider;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for ground contact in the Update method
        isGrounded = myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log("isGrounded = " + isGrounded);

        transform.Translate(speed, 0, 0);

        // Ollie logic
        if (isGrounded)
        {
            myAnimator.SetBool("isOllie", false);
            myAnimator.SetBool("isRolling", true);
        }
        else
        {
            myAnimator.SetBool("isRolling", false);
        }
    }

    void OnOllie(InputValue value)
    {
        // Perform ollie only if the skater is grounded
        if (isGrounded)
        {
            myAnimator.SetBool("isOllie", true);
            myRigidBody.velocity += new Vector2(0f, 4f);
            Debug.Log("Ollie animation triggered");
        }
    }

    void OnKickflip(InputValue value)
    {
        Debug.Log("Doing kickflip");
    }

    void OnHeelflip(InputValue value)
    {
        Debug.Log("Doing heelflip now");
    }

    void OnGrab(InputValue value)
    {
        Debug.Log("Doing grab now");
    }
}
