using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public GameObject playerA, playerB, firstPersonA, firstPersonB, thirdPersonA, thirdPersonB, playerABody, playerBBody;
    public float aMS = 0.1f, bMS = 0.1f;
    public int room = 0, playerARoom = 0, playerBRoom = 0;


    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -90F;
    public float maximumY = 90F;

    public float timer = 0;
    public float spinRate = 360f;

    float rotationY = 0F;

    // Start is called before the first frame update
    void Start()
    {
        initRoom0();
        firstPersonA.transform.localEulerAngles = Vector3.zero;
    }


    void mouseCamera(GameObject cam, GameObject player = null)
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = cam.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            if (player == null)
            {
                cam.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            } else
            {
                cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
                player.transform.localEulerAngles = new Vector3(0, rotationX, 0);
            }
        }
        else if (axes == RotationAxes.MouseX)
        {
            if (player == null)
            {
                cam.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            } else
            {
               player.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            if (player == null)
            {
                cam.transform.localEulerAngles = new Vector3(-rotationY, cam.transform.localEulerAngles.y, 0);
            } else
            {
                cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
            }
        }


        if (minimumX > -360 && maximumX < 360)
        {
            print(minimumX);
            if (cam.transform.localEulerAngles.y <= maximumX && cam.transform.localEulerAngles.y >= minimumX)
            {
                // ok
            }
            else if (cam.transform.localEulerAngles.y >= 360 + minimumX)
            {
                // ok
            }
            else
            {
                float high = cam.transform.localEulerAngles.y - maximumX;
                float low = cam.transform.localEulerAngles.y - (360 + minimumX);
                if (Mathf.Abs(high) < Mathf.Abs(low) && cam.transform.localEulerAngles.y >= minimumX)
                {
                    cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, maximumX, cam.transform.localEulerAngles.z);
                }
                else
                {
                    cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, minimumX, cam.transform.localEulerAngles.z);
                }
            }
        }


    }

    public void initRoom0()
    {
        sensitivityX = 15F;
        sensitivityY = 15F;
        minimumX = -360F;
        maximumX = 360F;
        minimumY = -60F;
        maximumY = 60F;
        playerBBody.SetActive(true);
        firstPersonA.SetActive(false);
        thirdPersonA.SetActive(false);
        thirdPersonB.SetActive(false);
        firstPersonB.SetActive(true);
        playerABody.SetActive(true);
        firstPersonB.transform.localEulerAngles = Vector3.zero;
        room = 0;
    }

    public void initRoom1()
    {
        sensitivityX = 15F;
        sensitivityY = 15F;
        minimumX = -90F;
        maximumX = 90F;
        minimumY = -60F;
        maximumY = 60F;
        playerABody.SetActive(false);
        firstPersonB.SetActive(false);
        thirdPersonA.SetActive(false);
        thirdPersonB.SetActive(false);
        firstPersonA.SetActive(true);
        playerBBody.SetActive(true);
        firstPersonA.transform.localEulerAngles = Vector3.zero;
        room = 1;
    }

    public void initRoom3()
    {
        sensitivityX = 15F;
        sensitivityY = 15F;
        minimumX = -360F;
        maximumX = 360F;
        minimumY = -60F;
        maximumY = 60F;
        playerABody.SetActive(false);
        firstPersonB.SetActive(false);
        thirdPersonA.SetActive(false);
        thirdPersonB.SetActive(false);
        firstPersonA.SetActive(true);
        playerBBody.SetActive(true);
        firstPersonA.transform.localEulerAngles = Vector3.zero;
        room = 3;
    }

    public void initRoom4()
    {
        playerBBody.SetActive(false);
        firstPersonA.SetActive(false);
        thirdPersonA.SetActive(false);
        thirdPersonB.SetActive(false);
        firstPersonB.SetActive(true);
        playerABody.SetActive(true);
        firstPersonB.transform.localEulerAngles = Vector3.zero;
        room = 4;
    }


    void Room0()
    {
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.A) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerA.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.W) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerA.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }


        //playerA.transform.eulerAngles = new Vector3(0, firstPersonA.transform.localEulerAngles.y, 0);

        /* Player B stuff */
        if (Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.LeftArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, firstPersonB.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.UpArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, firstPersonB.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }
        mouseCamera(firstPersonB);
    }


    void Room1()
    {
        /* Player A stuff */
        Vector3 moveDir = Vector3.zero;
        /*if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.A) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerA.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.W) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerA.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }*/

        
        //playerA.transform.eulerAngles = new Vector3(0, firstPersonA.transform.localEulerAngles.y, 0);

        /* Player B stuff */
        if (Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = new Vector3(!Input.GetKey(KeyCode.LeftArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerB.transform.eulerAngles.y + 180, 0);
            //moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
        {
            moveDir = new Vector3(!Input.GetKey(KeyCode.UpArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerB.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }
        mouseCamera(firstPersonA);

        playerBBody.transform.eulerAngles += new Vector3(0, Time.deltaTime * spinRate, 0);
    }


    void Room3()
    {


        /* Player A stuff */
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.A) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, firstPersonA.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.W) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, firstPersonA.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }

        mouseCamera(firstPersonA);
        //playerA.transform.eulerAngles = new Vector3(0, firstPersonA.transform.eulerAngles.y, 0);

        /* Player B stuff */
        if (Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.LeftArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerB.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.UpArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerB.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }
    }

    void Room4()
    {
        /* Player A stuff */
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.A) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerA.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.W) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, playerA.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerA.GetComponent<CharacterController>().Move(moveDir);
        }


        /* Player B stuff */
        if (Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.LeftArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, firstPersonB.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
        {
            moveDir = new Vector3(Input.GetKey(KeyCode.UpArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, firstPersonB.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= aMS; // movement speed
            playerB.GetComponent<CharacterController>().Move(moveDir);
        }

        mouseCamera(firstPersonB);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //playerB.transform.eulerAngles = new Vector3(0, timer * spinRate, 0);
        
        if (playerARoom != room)
        {
            if (playerARoom == playerBRoom)
            {
                switch (playerARoom)
                {
                    case 0:
                        {
                            initRoom0();
                            break;
                        }
                    case 1:
                        {
                            initRoom1();
                            break;
                        }

                    case 3:
                        {
                            initRoom3();
                            break;
                        }
                    case 4:
                        {
                            initRoom4();
                            break;
                        }
                }
            }
        }

        switch (room)
        {
            case 0:
                {
                    Room0();
                    break;
                }
            case 1:
                {
                    Room1();
                    break;
                }
            case 3:
                {
                    Room3();
                    break;
                }
            case 4:
                {
                    Room4();
                    break;
                }
        }

        
    }
}
