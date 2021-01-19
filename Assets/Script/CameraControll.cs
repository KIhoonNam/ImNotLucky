using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    float direction = Vector3.forward.x;
    [SerializeField]
    Transform Player;

    Quaternion ReRotation;
    Vector3 Retrans;
    public bool stop = false;
    float BackPosition;
    // Start is called before the first frame update
    void Start()
    {
        var quaternion = Player.rotation.y;
        BackPosition = quaternion;
        Retrans = transform.position;
        ReRotation =Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            if(Player.eulerAngles.y>=0 && Player.eulerAngles.y<180f)
                 BackPosition = -1;
            if(Player.eulerAngles.y >= 180 && Player.eulerAngles.y < 360f)
                BackPosition = 1;

            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.position.x +(BackPosition * -1.5f), Player.position.y + 0.748f, Player.position.z + (BackPosition* 1.5f)), 3f * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-12f, Player.rotation.eulerAngles.y + (-27f), 0), 3f * Time.deltaTime);
            
        }
        else if(!stop)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Retrans.x, Retrans.y, Retrans.z),3f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(ReRotation.eulerAngles.x, ReRotation.eulerAngles.y, ReRotation.eulerAngles.z), 3f * Time.deltaTime);
        }

    }
}
