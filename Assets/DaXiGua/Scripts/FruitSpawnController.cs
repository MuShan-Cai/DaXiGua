using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnController : MonoBehaviour
{

    [SerializeField]
    private FruitFactory fruitFactory;

    [SerializeField]
    private Canvas fruitCanvas;

    [SerializeField]
    private SafeLine safeLine;


    public Fruit WaitForFallFruit
    {
        get
        {
            return waitForFallFruit;
        }
    }

    private Fruit waitForFallFruit;
    //int index = 0;



    public static FruitSpawnController Instance
    {
        get
        {
            return instance;
        }
    }

    private static FruitSpawnController instance;
    
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
        //InvokeRepeating("SpawnFruit", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SpawnFruit(index++);
        }
    }

    public void SpawnFruit()
    {
        int random = Random.Range(0, 4);
        Vector3 randomPos = new Vector3(Random.Range(-1080 / 2, 1080 / 2), 1000, 0);
        var fruit = fruitFactory.GetFruit(random);
        fruit.transform.SetParent(fruitCanvas.transform);
        //fruit.transform.localPosition = randomPos;
        fruit.transform.localPosition = new Vector3(0, 1000, 0);
        fruit.transform.localScale = Vector3.one;
        fruit.GetComponent<Rigidbody2D>().simulated = false;
        waitForFallFruit = fruit;
    }

    public void FallWaitingFruit()
    {
        if(waitForFallFruit != null)
        {
            waitForFallFruit.ToFall();
            waitForFallFruit = null;
            if(GameManager.Instance.MyGameState.Equals(GameManager.GameState.Gaming))
            {
                Invoke("SpawnFruit", .5f);
            }
            safeLine.DisableForAWhile(2);
        }
    }

    public void ClearFruit()
    {
        StartCoroutine(ClearFruitIEn());
    }

    IEnumerator ClearFruitIEn()
    {
        for(int i=0;i<fruitCanvas.transform.childCount;i++)
        {
            var obj = fruitCanvas.transform.GetChild(i);
            var falledFruit = obj.GetComponent<Fruit>();
            if (falledFruit.gameObject.activeSelf)
            {
                falledFruit.DestroyFruit();
                yield return new WaitForSeconds(0.1f);
            }
        }
        PopupGenerator.Instance.OpenPopup<GameOverPopup>(PopupGenerator.PopupType.GameOverPopup.ToString(), null, 0.5f);

    }


}
