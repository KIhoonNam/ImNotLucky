using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Flop : UIBehaviour, IDragHandler
{
	public float Offset = 500f;
    GameObject[] Dices = null;
    public float radius;
    Image DiceChose;
    PlayerSprite Player;

    protected override void Awake()
    {
        Player = FindObjectOfType<PlayerSprite>();
        DiceChose = GameObject.Find("DiceChose").GetComponent<Image>();
        
    }
	protected override void Start()
	{
		base.Start();
        
        if (System.IO.File.Exists(Application.persistentDataPath + " Player"))
        {
            Player.LoadPlayer();
        }

        radius = Player.Stats.DiceIndex;
        Set();
		for (int i = 0; i < transform.childCount; i++)
		{
			var x = i * Offset;
			Drag(x, transform.GetChild(i));
		}
		Order();
	}

    public void Set()
    {
        Dices = new GameObject[(int)Player.Stats.DiceIndex];

        for(int i = 0; i < (int)Player.Stats.DiceIndex; i++)
        {
            var Dice = Resources.Load<GameObject>(Player.DN[i].name);

            Dices[i] = Instantiate(Dice);

            Dices[i].transform.SetParent(this.transform); 
        }
    }

	public void Drag(PointerEventData e)
	{
        if (transform.GetChild(transform.childCount-1).transform.localPosition.x <= radius*120f && transform.GetChild(transform.childCount - 1).transform.localPosition.x >= -(radius * 120f))
        {
            foreach (Transform i in transform)
            {

                var x = i.localPosition.x + e.delta.x;
                Drag(x, i);

            }
            Order();
        }
        else if(transform.GetChild(transform.childCount - 1).transform.localPosition.x > radius * 120f)
        {
            foreach (Transform i in transform)
            {

                var x = i.localPosition.x - 10;
                Drag(x, i);

            }
            Order();
        }
        else if(transform.GetChild(transform.childCount - 1).transform.localPosition.x < -(radius * 120f))
        {
            foreach (Transform i in transform)
            {

                var x = i.localPosition.x + 10;
                Drag(x, i);

            }
            Order();
        }

    }
	private void Order()
	{
		var children = GetComponentsInChildren<Transform>();
		var sorted = from child in children orderby child.localPosition.z descending select child;
		for (int i = 0; i < sorted.Count(); i++)
		{
			sorted.ElementAt(i).SetSiblingIndex(i);
            
		}
        DiceChose.sprite = Resources.Load<Sprite>("Dice/" + transform.GetChild(transform.childCount - 1).name) as Sprite;
	}
	public void Drag(float x, Transform t)
	{
        t.localPosition = new Vector3(x, transform.localPosition.y, x < 0 ? -x : x);
    }
	public void OnDrag(PointerEventData e)
	{
		Drag(e);
	}
}
