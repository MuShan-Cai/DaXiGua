using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Fruit : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem explodeEffect;
    [SerializeField]
    private int baseScore;
    [SerializeField]
    private int mergeExtraScore;
    public FruitFactory OriFruitFactory
    {
        get
        {
            return oriFruitFactory;
        }
        set
        {
            if (oriFruitFactory == null)
            {
                oriFruitFactory = value;
            }
        }
    }

    private FruitFactory oriFruitFactory;
    public int ID
    {
        get
        {
            return id;
        }

        set
        {
            if (id == int.MinValue && value != int.MinValue)
            {
                id = value;
            }
        }
    }

    private int id = int.MinValue;
    private Rigidbody2D rigidbody2D;
    private Image imgComponent;
    private Animator animator;
    private bool canMerge = false;
    private void OnEnable()
    {
        CanMergeTrue();
        if (rigidbody2D != null)
        {
            rigidbody2D.simulated = true;
        }

        if (imgComponent != null)
        {
            imgComponent.enabled = true;
        }

        if (animator != null)
        {
            animator.Play("Appear");
        }

    }


    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        imgComponent = GetComponentInChildren<Image>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canMerge)
        {
            return;
        }
        var fruit = collision.collider.GetComponent<Fruit>();
        if (fruit != null && fruit.ID == this.ID && fruit.ID < 10)
        {
            if (fruit.transform.position.y >= transform.position.y)
            {
                return;
            }
            canMerge = false;
            rigidbody2D.simulated = false;
            fruit.GetComponent<Rigidbody2D>().simulated = false;
            transform.SetAsLastSibling();
            var targetPos = fruit.transform.position;
            transform.DOMove(targetPos, 0.1f).OnComplete(() =>
            {
                DestroyFruit();
                fruit.Recycle();
                var newFruit = oriFruitFactory.GetFruit(ID + 1);
                if (newFruit != null)
                {
                    newFruit.transform.SetParent(fruit.transform.parent);
                    newFruit.transform.localPosition = fruit.transform.localPosition;
                    newFruit.transform.localScale = Vector3.one;
                }
                ScoreContro.Instance.AddScore(mergeExtraScore);
            });
        }
    }

    void Recycle()
    {
        oriFruitFactory.Recycle(this);
    }

    public void CanMergeTrue()
    {
        canMerge = true;
    }

    public void ToFall()
    {
        rigidbody2D.simulated = true;
    }

    public Rect GetImgRect()
    {
        return imgComponent.sprite.rect;
    }

    public void DestroyFruit()
    {
        imgComponent.enabled = false;
        rigidbody2D.simulated = false;
        explodeEffect?.Play();
        Invoke("Recycle", 1.2f);
        ScoreContro.Instance.AddScore(baseScore);
    }

}
