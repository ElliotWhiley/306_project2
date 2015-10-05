using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour
{
    public GameObject player;
    Vector3 lastPosition;
    Vector3 initialPosition;

    void Start()
    {
        lastPosition = player.transform.position;
        initialPosition = lastPosition;
    }

    void FixedUpdate()
    {

        float speedX = ((player.transform.position.x - lastPosition.x) / Time.deltaTime);
        float speedY = ((player.transform.position.y - lastPosition.y) / Time.deltaTime);

        if (Camera.main.transform.position == initialPosition)
        {
            float fX = player.transform.position.x - initialPosition.x;
            float fY = player.transform.position.y - initialPosition.y;
            transform.Translate(-fX, -fY, 0);
        }

        //if moved to the left
        if (speedX < 0) {
            transform.Translate(-Vector3.left * 5 *Time.deltaTime, player.transform);
        //if not moved from the start position
        } else if (speedX == 0) {
            //do nothing
        //if moved to the right
        } else {
            transform.Translate(-Vector3.right * 5 * Time.deltaTime, player.transform);
        }

        //if moving downwards
        if (speedY < 0)
        {
            transform.Translate(-Vector3.down * 5 * Time.deltaTime, player.transform);
        //if not moving vertically
        } else if (speedY == 0)
        {
            // do nothing
        //if moving upwards
        } else
        {
           transform.Translate(-Vector3.up * 5 * Time.deltaTime, player.transform);
        }

        lastPosition = player.transform.position;
    }
}