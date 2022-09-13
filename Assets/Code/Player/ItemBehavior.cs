using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public Transform hand;
    public Transform itemHolder;
    public bool hasItem = false;

    [SerializeField]
    Vector3 offset;

    GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Drop item
        if (Input.GetKey(KeyCode.E) && hasItem)
        {
            item.transform.parent = itemHolder;
            hasItem = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("item"))
        {
            // Destroy(collider.gameObject);
            item = collider.gameObject;
            //item.transform.parent = this.transform;
            item.transform.parent = hand;
            // Vector3 newHandPos = hand.position + offset;
            // if (hand.position.x != newHandPos.x)
            // {
            //     hand.position += offset;
            // }
            // item.transform.position = hand.position;
            item.transform.localPosition = hand.localPosition + offset;
            hasItem = true;
        }
    }
}
