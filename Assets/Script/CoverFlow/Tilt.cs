using UnityEngine;
using UnityEngine.UI;
public class Tilt : MonoBehaviour
{
	public Vector2 Range = new Vector2(3.33f, 3.33f);
	private Transform _t;
	private Quaternion _q;
	private Vector2 _r = Vector2.zero;
    public Text text;
    public Image image;
	private void Start()
	{
		_t = transform;
		_q = _t.localRotation;
        Character.Result += Result;
        Stage2.Result += Result;
        DiceStat.Result += Result;
	}
	private void Update()
	{
		Vector3 p = Input.mousePosition;
		float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;
		float x = Mathf.Clamp((p.x - halfWidth) / halfWidth, -1f, 1f);
		float y = Mathf.Clamp((p.y - halfHeight) / halfHeight, -1f, 1f);
		_r = Vector2.Lerp(_r, new Vector2(x, y), Time.deltaTime * 5f);
		_t.localRotation = _q * Quaternion.Euler(-_r.y * Range.y, _r.x * Range.x, 0f);
	}


    void Result(int n,string name)
    {
        image.enabled = true;
        text.enabled = true;
        text.text = name + n + "" ;

        Invoke("Invoke", 1f);
    }

    void Invoke()
    {
        image.enabled = false;
        text.enabled = false;
    }
    private void OnDestroy()
    {
        DiceStat.Result -= Result;
        Stage2.Result -= Result;
        Character.Result -= Result;
    }
}
