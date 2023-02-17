using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballPointsText : MonoBehaviour
{
    private float _speed = 1f;
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
    private void OnRenderObject()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * _speed;
        Invoke("DestroyThis", 0.5f);
    }
}
