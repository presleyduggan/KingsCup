using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float sx;
    public float sy;
    public float sz;
    public GameManager gm;
    public float speed;

    public GameObject pause;
    private Vector3 moveDirection = Vector3.zero;
    private float ox;
    private float oy;
    private float oz;



    // Start is called before the first frame update
    void Start()
    {
        Camera main;
        ox = 0.619f;
        oy = 4.97f;
        oz = 0.21f;
    }







    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();

        float x = 2 * Input.GetAxis("Mouse X");
        float y = 2 * -Input.GetAxis("Mouse Y");
        if ((pause.active == false) && gm.freecam == true)
        {
            Camera.main.transform.Rotate(y, x, 0);
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            controller.Move(moveDirection * Time.deltaTime);
        }



        // declare the RaycastHit variable

        if (Input.GetKeyDown(KeyCode.R))
        {
            
                FixCamera();
        }


    }



    public void FixCamera()
    {
        Camera.main.transform.eulerAngles = new Vector3(sx, sy, sz);
        Camera.main.transform.position = new Vector3(ox, oy, oz);
    }

}