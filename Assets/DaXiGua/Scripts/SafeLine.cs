using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeLine : MonoBehaviour
{

    private Collider2D collider2D;

    private Coroutine disableColliderCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableForAWhile(float duration)
    {
        if(disableColliderCoroutine != null)
        {
            StopCoroutine(disableColliderCoroutine);
        }
        disableColliderCoroutine = StartCoroutine(DisableForAWhileIEn(duration));
    }

    IEnumerator DisableForAWhileIEn(float duration)
    {
        collider2D.enabled = false;
        yield return new WaitForSeconds(duration);
        collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var fruit = collision.GetComponent<Fruit>();
        if(fruit!=null && GameManager.Instance.MyGameState.Equals(GameManager.GameState.Gaming))
        {
            Debug.Log("GameOver");
            GameManager.Instance.MyGameState = GameManager.GameState.GameOver;
        }
    }


}
