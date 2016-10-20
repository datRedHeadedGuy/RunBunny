using UnityEngine;
using System.Collections;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnimation;
    public float bunnyJumpForce = 500f;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if ( Input.GetButtonUp("Jump") )
        {
            myRigidBody.AddForce(transform.up * bunnyJumpForce);
        }

        myAnimation.SetFloat("vVelocity", myRigidBody.velocity.y);
        // myAnimation.SetFloat("vVelocity", Mathf.Abs(myRigidBody.velocity.y));
    }

    // 
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
