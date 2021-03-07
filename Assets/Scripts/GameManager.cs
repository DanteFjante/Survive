using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
    
    #endregion
    
    public List<Rigidbody2D> projectiles;
    public Transform spawnPoint;
    public float forceMultiplier;
    public CameraFollower cam;

    public int Score;

    private Vector3 _beginDrag;
    // Start is called before the first frame update
    void Start()
    {
        if(cam == null || spawnPoint == null)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _beginDrag = Input.mousePosition;
            LoadProjectile();
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            var force = (_beginDrag - Input.mousePosition) / 10f;
            if(force.magnitude > 10)
                ShootProjectile(Vector3.ClampMagnitude(force, 50) * forceMultiplier);
        }

    }

    void LoadProjectile()
    {
        if (projectiles.Count == 0)
            return;
        if (!GameObject.Find(projectiles[0].name))
            projectiles[0] = Instantiate(projectiles[0]);
        Rigidbody2D proj = projectiles[0];
        proj.position = spawnPoint.position;
        proj.constraints = RigidbodyConstraints2D.FreezeAll;
        cam.follow = proj.transform;
    }
    
    void ShootProjectile(Vector2 force)
    {
        if(projectiles.Count == 0)
            return;
        Rigidbody2D proj = projectiles[0];
        proj.constraints = RigidbodyConstraints2D.None;
        proj.AddForce(force * forceMultiplier);
        projectiles.Remove(proj);
    }
}
