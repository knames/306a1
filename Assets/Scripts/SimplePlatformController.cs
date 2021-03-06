﻿using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
    float groundRadius = 0.2f;
	public Transform spawn;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

    CircleCollider2D ccol2d;
    BoxCollider2D bcol2d;



    // Use this for initialization
    void Awake () {
		transform.position = spawn.position;
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.isKinematic = true;

        ccol2d = GetComponent<CircleCollider2D>();
        bcol2d = GetComponent<BoxCollider2D>();
		//rb2d.isKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
		rb2d.isKinematic = false; // crappy hack to prevent jumping
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
        //Debug.Log(grounded);
        if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		} else if (!grounded) { // animate when falling
			anim.SetTrigger ("Jump"); 
		}

		if (transform.position.y < -10) {
			//respawn
			rb2d.velocity = Vector2.zero;
			transform.position = spawn.position;
		}

        if (!grounded && rb2d.velocity.y > 0){
            ccol2d.enabled = false;
            bcol2d.enabled = false;
            //			boxCollider2D.enabled = false;
        }
        else{
            ccol2d.enabled = true;
            bcol2d.enabled = true;
            //			boxCollider2D.enabled = true;
        }

    }


	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (h));

		if (h * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce (Vector2.right * h * moveForce); // will work for left too since negative
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y); // Mathf sign: returns 1 if pos, 0 if neg
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();
		if (jump) {
			anim.SetTrigger ("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}


			                    
	}
}
