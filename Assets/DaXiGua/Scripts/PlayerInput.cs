using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{


    public static PlayerInput Instance
    {
        get
        {
            return instance;
        }
    }

    private static PlayerInput instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            throw new UnityException("已有实例：" + name);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var targetFruit = FruitSpawnController.Instance.WaitForFallFruit;

        if(targetFruit != null)
        {
            if (Input.GetMouseButton(0))
            {
                var targetPos = Input.mousePosition;
                targetPos.x = targetPos.x - Screen.width / 2;

                var imgRect = targetFruit.GetImgRect();
                targetPos.x = Mathf.Clamp(targetPos.x, -Screen.width / 2 + imgRect.width / 2, Screen.width / 2 - imgRect.width / 2);

                var newPos = new Vector3(targetPos.x, targetFruit.transform.localPosition.y, targetFruit.transform.localPosition.z);
                targetFruit.transform.localPosition = newPos;

            }
            if (Input.GetMouseButtonUp(0))
            {
                FruitSpawnController.Instance.FallWaitingFruit();
            }
        }


    }
}
