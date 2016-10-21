using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnimation;
    private float bunnyHurtTime = -1;
    private Collider2D myCollider;
    private float startTime;
    private int jumpsLeft = 2;
    public float bunnyJumpForce = 500f;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (bunnyHurtTime == -1)
        {
            if (Input.GetButtonUp("Jump") && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;
                }

                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce * 0.75f);
                }
                else
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce);
                    jumpsLeft--;
                }
            }

            myAnimation.SetFloat("vVelocity", myRigidBody.velocity.y);
            // myAnimation.SetFloat("vVelocity", Mathf.Abs(myRigidBody.velocity.y));

            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            if (Time.time > bunnyHurtTime + 2)
            {
                // Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene("Game");
            }
        }
    }

    // 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft lefter in FindObjectsOfType<MoveLeft>())
            {
                lefter.enabled = false;
            }

            bunnyHurtTime = Time.time;
            myAnimation.SetBool("bHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * bunnyJumpForce);
            myCollider.enabled = false;
            // Application.LoadLevel(Application.loadedLevel);
            // SceneManager.LoadScene("Game");
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
        }
    }
}
