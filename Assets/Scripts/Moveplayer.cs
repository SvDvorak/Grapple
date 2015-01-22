using UnityEngine;
using System.Collections;

public class Moveplayer : MonoBehaviour {

    public float speed = 1.0f;
    
    public string horizontal = "Horizontal";
  
    public Rigidbody2D rigidbody;
    public bool hasMoved;
    public bool jumping;
    public Vector3 lastPos;
    public Vector2 gravity;
   
    void Start()
    {
        gravity = Physics2D.gravity;
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        
        lastPos = gameObject.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & jumping == false)
        {
            
           jumping = true;
            gameObject.rigidbody2D.velocity = -gravity;
            
        }
        
        if (rigidbody.velocity.y <= 0 && lastPos.y <= gameObject.transform.position.y)
        {
            jumping = false;
        }
    }
    void FixedUpdate()
    {
        CheckIfCharacterMoved();
        transform.position += transform.right * Input.GetAxis(horizontal) * speed * Time.deltaTime;
    }

    public void CheckIfCharacterMoved()
    {
      
        if (lastPos == gameObject.transform.position)
        {
            hasMoved = false;
          
        }
        else if (lastPos.y != gameObject.transform.position.y)
        {
        
            
            
        }
        else if(lastPos.x < gameObject.transform.position.x)
        {
            hasMoved = true;
            
           
        }
        else if(lastPos.x > gameObject.transform.position.x)
        {
            hasMoved = true;
            
           
        }
       
       
        lastPos = gameObject.transform.position;
        
      
    }

    
   
              
          
    
}