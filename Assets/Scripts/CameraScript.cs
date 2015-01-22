using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    public Transform player;
    public float distance;
    public float horizontaldistance;
    public float verticaldistance;
    public float speed = 1.5f;
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(2))
        {
            var mousePositionInWorld = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            var mouseMoveDirection = (mousePositionInWorld-transform.position).normalized * speed;
            transform.position += new Vector3(mouseMoveDirection.x, mouseMoveDirection.y, 0);
            //new Vector3((player.position.x + Input.GetAxis("Mouse X")), (player.position.y + Input.GetAxis("Mouse Y")),(player.position.z - distance));
        }
        else
        {
            transform.position = new Vector3(player.position.x + horizontaldistance,
                player.position.y + verticaldistance, player.position.z - distance);
        }
    }
}
