using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamWalk : MonoBehaviour
{
    public GameObject dreamer, dreamCam;
    public RawImage dreamScreen;
    public float MS = 0.1f;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -90F;
    public float maximumY = 90F;
    float rotationY = 0F;

    public float fadeTime = 5.0f, timeUntilFade = 3f, timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void fading()
    {
        timer += Time.deltaTime;
        if(timer >= timeUntilFade)
        {
            float time2 = timer - timeUntilFade;
            if(time2 >= fadeTime)
            {
                dreamScreen.gameObject.SetActive(false);
                dreamer.SetActive(false);
                return;
            }
            dreamScreen.color = new Color(dreamScreen.color.r, dreamScreen.color.g, dreamScreen.color.b, 1 - (time2 / fadeTime));
        }
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
            }
            else
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
            }
            else
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
            }
            else
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

    // Update is called once per frame
    void Update()
    {
        fading();
        if (Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 moveDir = new Vector3(Input.GetKey(KeyCode.LeftArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, dreamCam.transform.eulerAngles.y + 180, 0);
            moveDir = rotation * moveDir; // rotating so left is appropriate
            moveDir *= MS; // movement speed
            dreamer.GetComponent<CharacterController>().Move(moveDir);
        }

        if (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 moveDir = new Vector3(Input.GetKey(KeyCode.UpArrow) ? 1 : -1, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, dreamCam.transform.eulerAngles.y - 90, 0);
            moveDir = rotation * moveDir; // rotating so forward is appropriate
            moveDir *= MS; // movement speed
            dreamer.GetComponent<CharacterController>().Move(moveDir);
        }
        mouseCamera(dreamCam);
    }
}
