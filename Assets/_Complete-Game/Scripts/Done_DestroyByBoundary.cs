using UnityEngine;
using System.Collections;

public class Done_DestroyByBoundary : MonoBehaviour
{
    private float radius;
    private float z_collision;
    private float x_collision;

    private Done_GameController gameController;
    private GameObject LineRenderGameObject;             // draw the line
    private LineRenderer lineRenderer;
    public static double angle, pre_angle, delta_angle;
    public static double dda;
    public SerialController serialController;              // read and write
    private int firsty;
    private Vector3 cur_pos;
    private int hrdy;


    void Start()
    {
        radius = 9.0f;
        pre_angle = 0;
        angle = 0;
        firsty = 0;
        //dda = 0;
    }
   
    void Update()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        string message;
        
        if (firsty == 0)
        {
            firsty = 1;
            message = serialController.ReadSerialMessage();
            if (message == null)
            {
                Debug.Log("Got nothing...");
                return;
            }
            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
                Debug.Log("Message arrived: " + message);
        }

        else
        {
            //receive the message from Arduino
            message = serialController.ReadSerialMessage();
            if (message == null)
            {
                  return;
            }
            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            {
                Debug.Log("Connection established...");
            }
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
            {
                
                if (double.TryParse(message, out dda))
                {
                    if(dda!=0)
                    Debug.Log("Get the angle:" + dda);
                }
                else
                {
                   // Debug.Log("error in the string..." + message + "||");      // receive some dirty data(maybe nothing), but wait to send...
                }
            }
        }

        // calculate the angle 
        z_collision = Done_GameController.z_collision;              // get the z-coor of the collision
        x_collision = Done_GameController.x_collision;
        
        angle = Mathf.Acos(x_collision / radius) * 180 / 3.142;
        if (z_collision < 0)
        {
            angle = 360 - angle;
        }
        delta_angle = angle - pre_angle;                  // + ccw   - cw
        if (delta_angle > 180)
            delta_angle = delta_angle - 360;
        else if (delta_angle < -180)
            delta_angle = 360 + delta_angle;

       /*LineRenderGameObject = GameObject.Find("line");           // draw the line before sending
       lineRenderer = (LineRenderer)LineRenderGameObject.GetComponent("LineRenderer");
       lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
       lineRenderer.SetPosition(1, new Vector3(x_collision, 9.3f, z_collision));*/

        // sending part...
        if((int)angle==89)
        {
           // Debug.Log("Get a 89, sending 0...");
            serialController.SendSerialMessage("0");
        }

        // send the data consistently...
        string xx = (delta_angle).ToString("0.00");              // send the data, without end_sign...
        serialController.SendSerialMessage(xx);
        if(delta_angle!=0)
        Debug.Log("sssending it:"+ xx);
        //Debug.Log("Send a x_cor & rr&angle&pre&delta: " + x_collision + "||" + radius + "||" + angle + "||" + pre_angle + "||" + delta_angle);       
        pre_angle = angle;
        // end of caculate the angle

        cur_pos = Done_Mover.cur_pos;
     //   if ((cur_pos[2] - z_collision) <= 0.3  && cur_pos[2]>=z_collision && cur_pos[2] != 0 )       // ready to send the crash message
        {
           // Debug.Log("now curpos2"+cur_pos[2]);
            
            // serialController.SendSerialMessage("777");
         //   Debug.Log("already send it&zz:"+ cur_pos[2]+"||"+z_collision);
            /*  Debug.Log("Boom...");
            if (gameObject.tag == "Enemy")
            {
                Destroy(gameObject);              
            }*/
        }
    }


    void OnCollisionExit(Collision other)
    {
        Destroy(other.gameObject);
    }
}