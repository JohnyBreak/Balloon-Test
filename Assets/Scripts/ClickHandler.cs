using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ClickHandler : MonoBehaviour
{
    public event Action<Balloon> BalloonClickedByPlayerEvent;

    [SerializeField] private LayerMask _baloonMask;
    private Camera _camera;
    // Start is called before the first frame update
    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.CurrentGameState != GameStateManager.GameState.GamePlay) return;

        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
        }
    }

    private void Raycast()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

        if (!hit) return;

        if (((1 << hit.collider.gameObject.layer) & _baloonMask) == 0) return;
        if (hit.collider.gameObject.TryGetComponent<Balloon>(out var balloon))
        {
            balloon.Click();
            BalloonClickedByPlayerEvent?.Invoke(balloon);
        }
    }
}
