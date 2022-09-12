using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

    private Vector3 playerCenter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("item"))
        {
            // Destroy(collider.gameObject);
            GameObject item = collider.gameObject;
            playerCenter = item.GetComponent< Renderer>().bounds.center;
            item.transform.parent = this.transform;
        }
    }
}
