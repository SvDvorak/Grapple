using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Transform player;
   
    public float bulletSpeed = 10f;
    public float nrOfBullets = 5f;
    public float spread = 0.1f;
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
            bullet.transform.position = player.position;
            bullet.AddComponent<Rigidbody2D>();
            bullet.AddComponent<SpriteRenderer>();
            SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("Sprites/Enviroment/ground0");
            bullet.rigidbody2D.angularVelocity = 400;
            bullet.rigidbody2D.isKinematic = true;
            Vector3 aim = (worldPosition - player.position).normalized;
            var spreadDirection = Vector3.Cross(aim, new Vector3(0, 0, 1));
            var playerVelocity = player.transform.rigidbody2D.velocity;
            var bulletIndex = i - (nrOfBullets/2);
            bullet.rigidbody2D.velocity = (aim + spreadDirection*spread*bulletIndex)*bulletSpeed;
            bullet.rigidbody2D.velocity = bullet.rigidbody2D.velocity + playerVelocity;


        }
    }
    


}
