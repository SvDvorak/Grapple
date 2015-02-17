using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public Vector2 CurrentVelocity;

    public Text Debugtext;
    public Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Debugtext.text ="X: "+rigidbody.velocity.x+" \n Y: "+rigidbody.velocity.y;
	    CurrentVelocity = rigidbody.velocity;
	}

    // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only). (Since v4.3)
    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (CurrentVelocity.y <= -25f)
        {
            Time.timeScale = 0;
            Debugtext.text = "You died!";
           
        }
    }


}
