using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class done_bound
{
    public float xMin, xMax, zMin, zMax;
}

public class Playercontroller : MonoBehaviour {

    public float speed;
    public done_bound boundary;
    public float tilt;
    public GameObject shot;
    public Transform shotspawn;
    public float fireRate;
    private float nextFire;

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
           Instantiate(shot, shotspawn.position, shotspawn.rotation);
           GetComponent<AudioSource>().Play();
        }

    }


    void FixedUpdate()
    {
        float movehorizontal = Input.GetAxis("Horizontal");
        float movevertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movehorizontal, 0.0f, movevertical);
        GetComponent<Rigidbody>().velocity = movement*speed;
       
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp( GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(GetComponent<Rigidbody>().velocity.z * (-tilt), 0.0f, GetComponent<Rigidbody>().velocity.x * (-tilt));
    }
	
}
