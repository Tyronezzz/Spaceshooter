using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handRotater : MonoBehaviour 
{
    private float delta_angle;
    private float pre_delta;
    private int firsty;
    private float angle_per_rot;           // fixedupdate(), 50 frames/s, set it the speed of the hand. 
    private float counter;
    private int getrdy;

	void Start ()                  // Use this for initialization
    {
       // pre_delta = 0;
        firsty = 0;
        angle_per_rot = 1.0f;             // Standard 50 degrees in a second for 1.0f
        counter = 0;
        getrdy = 0;
	}
	
	
	void FixedUpdate ()                  // Update is called once per frame
    {
        //Debug.Log("getangle:"+delta_angle);
        /*if (delta_angle == 777)
        {
            Debug.Log("Boom...");
            delta_angle = 0;
            return;
        }*/
        
        if(getrdy==0)
        {
            delta_angle = (float)Done_DestroyByBoundary.dda;
            if (delta_angle != 0)
                Debug.Log("ROTATE:" + (delta_angle) + "||");
            else
            {
            //    return;
            }
               
        }

      //  Debug.Log("|||" + pre_delta + "||" + (delta_angle) + "||");  

        if (firsty == 0)
        {
            firsty++;
            transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), -89);       // set the watch hand at (0, -R) at first  
            return;
        }

        else if (firsty == 1 && pre_delta != delta_angle && (int)delta_angle != 0)
        {
           // firsty++;
           // transform.RotateAround(new Vector3(27, -9.3f, 0), new Vector3(0, -1, 0), delta_angle-180);
            getrdy = 1;
            if (delta_angle != 0 && firsty != 0)
            {
             // Debug.Log("|||"+ counter+"||"+(180-delta_angle)+"||");              
              if(delta_angle >=0)        // left
              {
                  if (counter <= 180-delta_angle - angle_per_rot)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), (-1)*angle_per_rot);
                      counter += angle_per_rot;  
                  }

                  else if (Mathf.Abs(delta_angle - 180) - counter >= 0 && Mathf.Abs(delta_angle - 180) - counter < angle_per_rot)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), -180+ delta_angle + counter);
                     // Debug.Log("this is now&sum:" + counter*(-1) + "||" + (180-delta_angle));
                      counter = 0;
                      firsty++;
                      pre_delta = delta_angle;
                      getrdy = 0;
                  }
                  
              }
              
              
              else if(delta_angle<0)    //right
              {
                  if (counter <= delta_angle - 180 - angle_per_rot + 360)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), angle_per_rot);
                      counter += angle_per_rot;  
                  }

                  else if (delta_angle - 180 + 360 - counter >= 0 && delta_angle - 180 + 360 - counter < angle_per_rot)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), (delta_angle - 180 - counter));
                    //  Debug.Log("this is now&sum:" + counter + "||" + (delta_angle + 180));
                      counter = 0;
                      firsty++;
                      pre_delta = delta_angle;
                      getrdy = 0;
                  }
                  
              }                        
            }        
        }

        else if (pre_delta != delta_angle && (int)delta_angle!=0)
        {
             //transform.RotateAround(new Vector3(27, -9.3f, 0), new Vector3(0, -1, 0), delta_angle);
             //Debug.Log("Get it as:" + delta_angle);
              getrdy = 1;       
              if(delta_angle >=0)        // righht
              {
                  if (counter <= delta_angle - angle_per_rot)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), angle_per_rot);
                      counter += angle_per_rot;  
                  }

                  else if (delta_angle - counter >= 0 && delta_angle - counter < angle_per_rot)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), delta_angle - counter);
                      //Debug.Log("this is now&sum:" + counter + "||" + delta_angle);
                      counter = 0;
                      firsty++;
                      pre_delta = delta_angle;
                      getrdy = 0;
                  }
                  
              }
                          
              else if(delta_angle<0)    //leeft
              {
                  if (counter <= (-1)*delta_angle - angle_per_rot)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), (-1)*angle_per_rot);
                      counter += angle_per_rot;
                  }

                  else if (delta_angle*(-1) - counter >= 0 && delta_angle*(-1) - counter < angle_per_rot)
                  {
                      transform.RotateAround(new Vector3(0, -9.3f, 0), new Vector3(0, -1, 0), (delta_angle+ counter));
                     // Debug.Log("this is now&sum:" + counter + "||" + (delta_angle));
                      counter = 0;
                      firsty++;
                      pre_delta = delta_angle;
                      getrdy = 0;
                  }
                 
              }
        }
	}
}
