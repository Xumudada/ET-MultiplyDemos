using UnityEngine;

namespace ETModel
{
    public class BackgroundImageSet
    {
        public static void BackgroundImageOn()
        {
            GameObject Root = GameObject.Find("Global/UI/LoginCanvas/Panel");
            if (Root != null && !Root.activeSelf)
            {
                //Root.GetComponent<Image>().enabled = true;
                //打开object
                Root.SetActive(true);
            }
        }

        public static void BackgroundImageOff()
        {
            GameObject Root = GameObject.Find("Global/UI/LoginCanvas/Panel");
            if (Root != null && Root.activeSelf)
            {
                //Root.GetComponent<Image>().enabled = false;
                //关闭object
                Root.SetActive(false);
            }
        }
    }

}