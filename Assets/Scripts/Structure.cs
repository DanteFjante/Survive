using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Structure : MonoBehaviour
{
    private PolygonCollider2D collider;
    
    public float maxDurability;

    public int ScoreWorth;
    
    private float durability;
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        Init();
    }

    public void Init()
    {        
        durability = maxDurability;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        float speed = other.relativeVelocity.magnitude;
        durability = durability - speed;

        if(durability < 0)
        {
            GameManager.Instance.Score += ScoreWorth;
            gameObject.SetActive(false);
        }
    }
}