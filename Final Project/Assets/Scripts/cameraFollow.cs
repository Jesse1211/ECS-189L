using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mangedCamera;
    [SerializeField] private GameObject Target;
    [SerializeField] public Vector2 minPosition;
    [SerializeField] public Vector2 maxPosition;
    [SerializeField] public float smoothing;
    private void Awake()
    {
        mangedCamera = gameObject.GetComponent<Camera>();
       
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        var targetPosition = this.Target.transform.position;
        var cameraPosition = mangedCamera.transform.position;
        cameraPosition = new Vector3 (targetPosition.x, targetPosition.y, -90); 
        
       
        if (targetPosition != cameraPosition)
        {
            Debug.Log("here");
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            cameraPosition = Vector3.Lerp(cameraPosition, targetPosition, smoothing);
            mangedCamera.transform.position = cameraPosition;
        }
        
    }
}
