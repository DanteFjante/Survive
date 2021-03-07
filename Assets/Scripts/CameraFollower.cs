using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    private Camera _camera;
    public Transform follow;

    public Vector2 padding;

    public float scrollSize = 1f;
    public float updateRate = .1f;

    public float minZoom = 1;
    public float maxZoom = 40;

    public Rect CameraMoveConstraints;


    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
        padding.x = Mathf.Clamp(padding.x, 0, 0.49999f);
        padding.y = Mathf.Clamp(padding.y, 0, 0.49999f);
        StartCoroutine(FollowRoutine());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.mouseScrollDelta != Vector2.zero)
        {

                _camera.orthographicSize = 
                    Mathf.Clamp(_camera.orthographicSize + (Input.mouseScrollDelta.y * scrollSize), 
                        minZoom, maxZoom);
        }

    }

    IEnumerator FollowRoutine()
    {

        while (gameObject.activeSelf)
        {
            if (follow != null)
            {
                var vp = _camera.WorldToViewportPoint(follow.position);
                float x = vp.x;
                float y = vp.y;

                if (vp.x < padding.x) x = padding.x;
                if (vp.x > 1 - padding.x) x = 1 - padding.x;

                if (vp.y < padding.y) y = padding.y;
                if (vp.y >= 1 - padding.y) y = 1 - padding.y;

                var np = new Vector3(
                    x,
                    y,
                    vp.z);

                if (np != vp)
                {
                    var wp = _camera.ViewportToWorldPoint(np);


                    var newPos = transform.position + follow.position - wp;

                    newPos.z = transform.position.z;
                    var aspect = _camera.aspect;
                    var ortho = new Vector2(_camera.orthographicSize * 2, _camera.orthographicSize * 2 * (1 / aspect));
                    newPos.x = Mathf.Clamp(newPos.x, CameraMoveConstraints.xMin + ortho.x, CameraMoveConstraints.xMax - ortho.x);
                    newPos.y = Mathf.Clamp(newPos.y, CameraMoveConstraints.yMin + ortho.y, CameraMoveConstraints.yMax - ortho.y);

                    transform.position = newPos;
                }
            }

            yield return new WaitForSeconds(updateRate);
            
        }

        yield return null;
    }

}
