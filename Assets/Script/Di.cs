using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Di : MonoBehaviour
{
    public static event RollDice DiceRoll;
    static Rigidbody rb;
    public static Vector3 DiceVelocity;
    GameManager gg;
    SoundScript sound;
    MeshRenderer mesh;
    Flop flop;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<SoundScript>();
        rb = GetComponent<Rigidbody>();
        gg = FindObjectOfType<GameManager>();
        mesh = transform.GetComponentInChildren<MeshRenderer>();

        flop = FindObjectOfType<Flop>();
    }



   // Update is called once per frame
    void Update()
    {
        DiceVelocity = rb.velocity;
        //if (Input.GetKeyDown(KeyCode.Space) && !gg.isMoving && !gg.EndMoving && !gg.Break)
        //{
        //    sound.Play();
        //    DiceCheckZoneScript.diceNumber = 0;
        //    float dirX = Random.Range(0, 100);
        //    float dirY = Random.Range(0, 100);
        //    float dirZ = Random.Range(0, 100);
        //    transform.position = new Vector3(-3, 2, -5);
        //    transform.rotation = Quaternion.Euler(Random.Range(10, 35), Random.Range(10, 35), Random.Range(10, 35));
        //    rb.AddForce(new Vector3(0, Random.Range(100, 200), 0));
        //    rb.AddTorque(dirX, dirY, dirZ);

        //    DiceRoll();


        //}

    }

    public void Roll()
    {
        
        sound.Play();
        DiceCheckZoneScript.diceNumber = 0;
        float dirX = Random.Range(0, 200);
        float dirY = Random.Range(0, 200);
        float dirZ = Random.Range(0, 200);
        transform.position = new Vector3(-3, 2, -5);
        transform.rotation = Quaternion.Euler(Random.Range(10, 90), Random.Range(10, 90), Random.Range(10, 90));
        rb.AddForce(new Vector3(0, Random.Range(100, 200), 0));
        rb.AddTorque(dirX, dirY, dirZ);
        this.name = flop.transform.GetChild(flop.transform.childCount - 1).name;
        mesh.material = Resources.Load<Material>("DiceMesh/" + flop.transform.GetChild(flop.transform.childCount - 1).name) as Material;
        
         DiceRoll();
    }
}
