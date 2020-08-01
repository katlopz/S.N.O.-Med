using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    private Vector3 playerPosition;
    public float offsetSmoothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //no look ahead, player in center
        //transform.position = new Vector3(player.transform.position.x,transform.position.y, transform.position.z);

        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        if(player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z); //adding offset
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z); 
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime); // from, to, how long it will take 
        //smaller offsetSmoothing, the slower the transition
        // if player is jittering, experiment with Interpolate on player's Rigidbody component either none/Interpolate/Exterpolate
       
    }
}
