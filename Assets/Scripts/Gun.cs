using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Transform playerposition;
    public float bulletSpeed = 10;
    public float nrOfBullets = 5;
    public float spread = (float)0.1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Fire();
        }
    }

    public void Fire()
    {
        GameObject bullet;
        var mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        for (int i = 0; i <= nrOfBullets; i++)
        {
            bullet = new GameObject("Bullet");
            bullet.transform.position = playerposition.position;
            bullet.AddComponent<Rigidbody2D>();
            bullet.AddComponent<SpriteRenderer>();
            SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<UnityEngine.Sprite>("Sprites/Enviroment/ground0");
            bullet.rigidbody2D.angularVelocity = 400;
            bullet.rigidbody2D.isKinematic = true;
            Vector3 aim = (worldPosition - playerposition.position).normalized;
          
            aim.y = (float)(aim.y * (i * spread));
            bullet.rigidbody2D.velocity = aim * bulletSpeed;


        }
    }
    


}
