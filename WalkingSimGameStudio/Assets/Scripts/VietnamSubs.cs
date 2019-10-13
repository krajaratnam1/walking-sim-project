using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VietnamSubs : MonoBehaviour
{
    public Text text;
    public string origString;
    public float subProb = 0.5f;
    public float timeToSub = 0.1f, timer = 0;

    string subChar(string c)
    {
        if (c == "A")
        {
            string[] subArray = { "Ă", "Â", "Ằ", "Ầ", "Ẳ", "Ẩ", "Ẵ", "Ẫ", "Ắ", "Ấ", "Ặ", "Ậ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "a")
        {
            string[] subArray = { "ă", "â", "ằ", "ầ", "ẳ", "ẩ", "ẵ", "ẫ", "ắ", "ấ", "ặ", "ậ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "E")
        {
            string[] subArray = { "Ê", "Ề", "Ể", "Ễ", "Ế", "Ệ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "e")
        {
            string[] subArray = { "ê", "ề", "ể", "ễ", "ế", "ệ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "I")
        {
            string[] subArray = { "Ì", "Ỉ", "Ĩ", "Í", "Ị" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "i")
        {
            string[] subArray = { "ì", "ỉ", "ĩ", "í", "ị" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "O")
        {
            string[] subArray = { "Ô", "Ồ", "Ổ", "Ỗ", "Ố", "Ộ", "Ơ", "Ờ", "Ở", "Ỡ", "Ớ", "Ợ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "o")
        {
            string[] subArray = { "ô", "ồ", "ổ", "ỗ", "ố", "ộ", "ơ", "ờ", "ở", "ỡ", "ớ", "ợ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "U")
        {
            string[] subArray = { "Ư", "Ừ", "Ử", "Ữ", "Ứ", "Ự" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "u")
        {
            string[] subArray = { "ư", "ừ", "ử", "ữ", "ứ", "ự" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "Y")
        {
            string[] subArray = { "Ỳ", "Ỷ", "Ỹ", "Ý", "Ỵ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "y")
        {
            string[] subArray = { "ỳ", "ỷ", "ỹ", "ý", "ỵ" };
            float rand = Random.value * subArray.Length;
            int ind = (int)rand;
            if (ind == subArray.Length)
            {
                ind--;
            }
            return subArray[ind];
        }

        else if (c == "D")
        {
            return "Đ";
        }

        else if (c == "d")
        {
            return "đ";
        }

        return c;
    }

    string subString()
    {
        string ret = "";
        for(int i = 0; i<origString.Length; i++)
        {
            float rand = Random.value;
            if(rand <= subProb)
            {
                ret += subChar(origString.Substring(i, 1));
            } else
            {
                ret += origString.Substring(i, 1);
            }
        }
        return ret;
    }

    // Start is called before the first frame update
    void Start()
    {
        origString = text.text; // temp
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeToSub)
        {
            timer = 0;
            text.text = subString();
        }
    }
}
