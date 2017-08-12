using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;
    public static Vector3 cur_pos; 
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

    void Update()
    {
        cur_pos = GetComponent<Rigidbody>().position; 
    }
   

}
