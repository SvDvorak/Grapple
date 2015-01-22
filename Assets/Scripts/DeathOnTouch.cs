using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathOnTouch : MonoBehaviour
{
    public Text AnnounceDeathText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only). (Since v4.3)
    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(coll.gameObject);
            AnnounceDeathText.text = "You died :(";
            Time.timeScale = 0;
        }
    }
    
   
}
