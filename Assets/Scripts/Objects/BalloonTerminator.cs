using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTerminator : MonoBehaviour
{
    public event Action<Balloon> BalloonDestroyedByTerminatorEvent;
    [SerializeField] private LayerMask _baloonMask;

    void Awake()
    {
        SetUp();
    }

    private void SetUp()
    {
        SetUpPosition();
        SetUpSize();
    }

    private void SetUpPosition()
    {
        var collider = GetComponent<BoxCollider2D>();

        Vector3 tempPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height));
        tempPosition.z = 0;
        tempPosition.y += collider.transform.localScale.y;// * 2.5f;
        transform.localPosition = tempPosition;
    }

    private void SetUpSize()
    {
        var collider = GetComponent<BoxCollider2D>();

        var screenWidth = (Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height);
        collider.transform.localScale = new Vector3((float)screenWidth, collider.transform.localScale.y, collider.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _baloonMask) == 0) return;

        if (collision.gameObject.TryGetComponent<Balloon>(out var balloon))
        {
            balloon.BackToPool();
            BalloonDestroyedByTerminatorEvent?.Invoke(balloon);
        }
    }
}
