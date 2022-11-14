using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descripter : MonoBehaviour
{
    public string type;
    public string shape;
    public string text;
    public float stable_time;
    public float respawn_time;
    public float radius;
    private float rotate_speed = 0.01f;
    private float test = 0f;
    private bool yes = false;


    //rotate around the w axis slowly
    void FixedUpdate()
    {
        if (yes)
        {
            float rot = transform.rotation.w + rotate_speed;

            Quaternion new_rot = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, rot);

            this.transform.rotation = new_rot;


            test = test + 1f;

            if (test >= 500)
            {
                rot = 0f;
                Quaternion reset = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, 0);
                this.transform.rotation = reset;
                test = 0f;
            }
        }
    }

}
