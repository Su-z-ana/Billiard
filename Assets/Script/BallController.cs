using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // main ball
    [SerializeField] GameObject mainBall = null;
    // force for strike
    [SerializeField] float power = 0.1f;
    // Transform for displaying the cue ball direction 
    [SerializeField] Transform arrow = null;
    // list of balls
    //[SerializeField] List<ColorBall> ballList = new List<ColorBall>();

    // mouse position strage
    Vector3 mousePosition = new Vector3();
    // Rigidbody for main ball
    Rigidbody mainRigid = null;
    // store main ball position when game is resetting 
    Vector3 mainBallDefaultPosition = new Vector3();

    void Start()
    {
        mainRigid = mainBall.GetComponent<Rigidbody>();
        mainBallDefaultPosition = mainBall.transform.localPosition;
        arrow.gameObject.SetActive(false);
    }

    void Update()
    {
        // when main ball is active
        if (mainBall.activeSelf == true)
        {
            // mouse click start
            if (Input.GetMouseButtonDown(0) == true)
            {
                // store start position
                mousePosition = Input.mousePosition;
                // display direction line
                arrow.gameObject.SetActive(true);
                Debug.Log("click started");
            }

            // during mouse button pressed
            if (Input.GetMouseButton(0) == true)
            {
                // store current position for each frame
                Vector3 position = Input.mousePosition;

                // calculate angle
                Vector3 def = mousePosition - position;
                float rad = Mathf.Atan2(def.x, def.y);
                float angle = rad * Mathf.Rad2Deg;
                Vector3 rot = new Vector3(0, angle, 0);
                Quaternion qua = Quaternion.Euler(rot);

                // set angle of direction line
                arrow.localRotation = qua;
                arrow.transform.position = mainBall.transform.position;
            }

            // end of mouse click
            if (Input.GetMouseButtonUp(0) == true)
            {
                // store the end position
                Vector3 upPosition = Input.mousePosition;

                // vector from start position to end position for calculating the direction of striking cue ball 
                Vector3 def = mousePosition - upPosition;
                Vector3 add = new Vector3(def.x, 0, def.y);

                // force on the main ball
                mainRigid.AddForce(add * power);

                // hiding the direction line
                arrow.gameObject.SetActive(false);

                Debug.Log("finish clicking");
            }
        }
    }

    // ---------------------------------------------------------------------
    /// <summary> 
    /// Reset button is clicked, callback function
    /// </summary> 
    // --------------------------------------------------------------------- 
    public void OnResetButtonClicked()
    {
        //Filled out later in this session
    }
}
