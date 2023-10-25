using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skater : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;

    [SerializeField]
    int pointsForTricks = 500;
    int pointsForOllie = 250;
    Animator myAnimator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCapsuleCollider;

    BoxCollider2D myBoxCollider;
    bool isGrounded = false;
    bool isOllieTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for ground contact in the Update method
        isGrounded = myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        transform.Translate(speed, 0, 0);

        // Ollie logic
        if (isGrounded)
        {
            myAnimator.SetBool("isRolling", true);
            myAnimator.SetBool("isHeelflip", false);
            myAnimator.SetBool("isKickflip", false);
            myAnimator.SetBool("isGrab", false);
            if (!isOllieTriggered)
            {
                myAnimator.SetBool("isOllie", false);
                myAnimator.SetBool("isRolling", true);
            }
        }
        else
        {
            myAnimator.SetBool("isRolling", false);
        }
    }

    void OnOllie(InputValue value)
    {
        // Perform ollie only if the skater is grounded and the ollie is not already triggered
        //Note: Ollie score works but we don't want to add ollie score if player also does trick
        //This is because we already have a trick score set.
        if (isGrounded && !isOllieTriggered)
        {
            isOllieTriggered = true;
            myAnimator.SetBool("isOllie", true);
            myRigidBody.velocity += new Vector2(0f, 4f);
            Debug.Log("Ollie animation triggered");
            StartCoroutine(OllieCooldown());
            FindObjectOfType<GameSession>().addToScore(pointsForOllie);
        }
    }

    IEnumerator OllieCooldown()
    {
        yield return new WaitForSeconds(0.08f);
        isOllieTriggered = false;
    }

    void OnHeelflip(InputValue value)
    {
        if (!isGrounded && myAnimator.GetBool("isOllie"))
        {
            myAnimator.SetBool("isHeelflip", true);
            Debug.Log("Doing heelflip now");
            FindObjectOfType<GameSession>().addToScore(pointsForTricks);
        }
    }

    void OnKickflip(InputValue value)
    {
        if (!isGrounded && myAnimator.GetBool("isOllie"))
        {
            myAnimator.SetBool("isKickflip", true);
            Debug.Log("Doing kickflip");
            FindObjectOfType<GameSession>().addToScore(pointsForTricks);
        }
    }

    void OnGrab(InputValue value)
    {
        {
            myAnimator.SetBool("isGrab", true);
            Debug.Log("Doing grab");
            FindObjectOfType<GameSession>().addToScore(pointsForTricks);
        }
    }

    public void playerFail()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            Debug.Log("Bailed");
        }
    }
}
