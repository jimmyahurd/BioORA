using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour    
{
    /*
    GameObject sphere1;
    GameObject sphere2;
    GameObject cylinder1;
    */

    public string fileName;
    public GameObject loadField;
    public GameObject UI;

    public GameObject protein;
    GameObject displayedProtein;

    private bool cameraLocked = false;

    public float cameraSensitivity = 90;
    public float climbSpeed = 4;
    public float moveSpeed = 10;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;


    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void FixedUpdate()
    {

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    displayedProtein = Instantiate(protein, new Vector3(0, 0, 0), Quaternion.identity);
        //    displayedProtein.GetComponent<Protein>().makeProtein("Assets/Resources/3pgk.pdb");
        //}

        /*
         * Camera controls, x stands for the x axis, and y for the  axis. Uses time to help with the speed at which the camera will move.
         * the controls are W for forward, S for back and the mouse controls left or right. Q, and E allow the user to go directly up, or directly down. 
         *
        */
        if (!cameraLocked)
        {


            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

            transform.position += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;

            if (Input.GetKey(KeyCode.Q))
            {
                transform.position += transform.up * climbSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.position -= transform.up * climbSpeed * Time.deltaTime;
            }
        }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UI.SetActive(!UI.activeSelf);
                cameraLocked = !cameraLocked;
            }
        
    }
    public void OnLoadButtonPress()
    {
        fileName = loadField.GetComponent<Text>().text;
        displayedProtein = Instantiate(protein, new Vector3(0, 0, 0), Quaternion.identity);
        displayedProtein.GetComponent<Protein>().makeProtein("Assets/Resources/" + fileName);
    }
    public void OnDeleteButtonPress()
    {
        displayedProtein.GetComponent<Protein>().delete();
    }
    public void OnExitButtonPress()
    {
        Application.Quit();
    }
}
