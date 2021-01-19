using UnityEngine;
public class LookAt : MonoBehaviour
{
    
    
    public float Damping = 101000;
    public bool stop = false;
   

    private void Start()
    {
        
    }
    protected void LateUpdate()
    {
        
            
               
       
        if (transform.localPosition.x >= -250 && transform.localPosition.x <= 250)
        {
            transform.localScale = new Vector3(1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f);

        }

        

        
                

              
         
    }
}
