using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    public Text text;
    public Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    text.text = rigidbody.velocity.ToString();
	}

    // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only). (Since v4.3)
    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (rigidbody.velocity.y > 35)
        {
            Time.timeScale = 0;
        }
    }


}
