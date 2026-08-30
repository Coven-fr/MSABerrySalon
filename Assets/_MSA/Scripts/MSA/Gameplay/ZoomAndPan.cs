using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ZoomAndPan : GameComponent
{
    Camera cam;

    [SerializeField] Transform target;
    Vector3 initialCamPosition;
    float initialCamProjection;

    [Header("Zoom")]

    [SerializeField] float zoomSpeed = 1f;
    [SerializeField] float smoothZoomSpeed = 2f;
    [SerializeField] float minZoom = 5f;
    [SerializeField] float maxZoom = 20f;
    float previousZoom;
    bool isZoomMin;
    bool isZoomMax;

    [Space(10)]

    public UnityEvent onZoom;

    [Header("Pan")]

    [SerializeField] float panSpeed = 0.1f;
    [SerializeField] float limitsMultiplier = 2f;
    [SerializeField] float dragThreshold = 10f;
    Vector2 minPanLimit;      
    Vector2 maxPanLimit;

    [Space(10)]

    public UnityEvent onPan;

    [Header("Events")]

    [SerializeField] ZoomEventChannel zoomEvent;

    float lastPinchDistance;
    Vector2 lastTouchPanPosition;
    Vector2 touchStartPosition;
    bool isDragging = false;
    bool isPinching = false;

    Vector2 lastMousePanPosition;

    void Awake()
    {
        cam = Camera.main;

        initialCamPosition = cam.transform.position;
        initialCamProjection = cam.orthographicSize;

        previousZoom = initialCamProjection;
    }

    void Update()
    {
        if (!isActive) return;

        HandleMouseInput();
        HandleTouchInput();
    }

    void HandleMouseInput()
    {
        if(Mouse.current == null) return; 

        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll != 0)
        {
            HandleZoom(scroll);
        }

        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            lastMousePanPosition = Mouse.current.position.ReadValue();
        }

        if (Mouse.current.middleButton.isPressed)
        {
            Vector2 currentPosition = Mouse.current.position.ReadValue();
            Vector2 delta = currentPosition - lastMousePanPosition;

            lastMousePanPosition = currentPosition;

            if (delta != Vector2.zero)
                HandlePan(delta);
        }
    }

    void HandleTouchInput()
    {
        if(Touchscreen.current == null) return;

        var touches = Touchscreen.current.touches;

        TouchControl firstTouch = null;
        TouchControl secondTouch = null;

        int activeTouches = 0;

        foreach (var touch in touches)
        {
            if (!touch.isInProgress) continue;

            if (firstTouch == null)
                firstTouch = touch;
            else if (secondTouch == null)
                secondTouch = touch;

            activeTouches++;

            if (activeTouches >= 2) break;
        }

        if (activeTouches == 1)
        {
            HandleSingleTouch(firstTouch);
        }
        else if (activeTouches >= 2)
        {
            HandlePinch(firstTouch, secondTouch);
        }
        else
        {
            isDragging = false;
            isPinching = false;
        }
    }

    void HandleSingleTouch(TouchControl touch)
    {
        Vector2 position = touch.position.ReadValue();

        if (touch.press.wasPressedThisFrame)
        {
            touchStartPosition = position;
            lastTouchPanPosition = position;
            isDragging = false;
            isPinching = false;
        }

        if (!touch.isInProgress) return;

        float distance =
            Vector2.Distance(
                position,
                touchStartPosition
            );

        if (!isDragging && distance > dragThreshold)
            isDragging = true;

        if (isDragging)
        {
            Vector2 delta = position - lastTouchPanPosition;

            lastTouchPanPosition = position;

            HandlePan(delta);
        }
    }

    void HandlePinch(TouchControl firstTouch, TouchControl secondTouch)
    {
        isDragging = false;

        Vector2 firstPosition = firstTouch.position.ReadValue();
        Vector2 secondPosition = secondTouch.position.ReadValue();

        float currentDistance = Vector2.Distance(firstPosition, secondPosition);

        if (!isPinching)
        {
            lastPinchDistance = currentDistance;
            isPinching = true;
            return;
        }

        float delta = lastPinchDistance - currentDistance;

        lastPinchDistance = currentDistance;

        float dpi = Screen.dpi > 0 ? Screen.dpi : 160f;
        float normalizedDelta = (delta / dpi) * 10f;

        HandleZoom(-normalizedDelta);
    }

    void HandleZoom(float zoomValue)
    {
        float newZoom;

        Vector3 pointerScreenPosition = GetPointerScreenPosition();
        Vector3 pointerWorldPositionBefore = cam.ScreenToWorldPoint(pointerScreenPosition);

        newZoom = cam.orthographicSize - zoomValue * zoomSpeed;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        cam.orthographicSize = newZoom;

        if (newZoom != previousZoom)
        {
            Vector3 pointerWorldPositionAfter = cam.ScreenToWorldPoint(pointerScreenPosition);
            Vector3 move = pointerWorldPositionBefore - pointerWorldPositionAfter;
            cam.transform.position += move;
        }

        if (newZoom > previousZoom && !isZoomMax)
        {
            CenterCameraSmoothly();
        }

        UpdateZoomState();

        previousZoom = newZoom;

        onZoom?.Invoke();
    }

    void HandlePan(Vector2 delta)
    {
        AdjustCameraBounds();

        Vector3 move = new Vector3(-delta.x, -delta.y, 0) * panSpeed;
        Vector3 newPosition = cam.transform.position + move;

        newPosition.x = Mathf.Clamp(newPosition.x, minPanLimit.x, maxPanLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minPanLimit.y, maxPanLimit.y);

        cam.transform.position = newPosition;

        onPan?.Invoke();
    }

    void CenterCameraSmoothly()
    {
        if (target == null) return;

        float zoomFactor = Mathf.InverseLerp(minZoom, maxZoom, cam.orthographicSize);
        float smoothFactor = Mathf.Pow(zoomFactor, 2);
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, cam.transform.position.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothZoomSpeed * smoothFactor * Time.deltaTime);
    }

    void AdjustCameraBounds()
    {
        if (target == null) return;

        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null) return;
        
        Vector2 spriteSize = spriteRenderer.bounds.size;
        float cameraHeight = cam.orthographicSize * 2;
        float cameraWidth = cameraHeight * cam.aspect;
        float halfSpriteWidth = spriteSize.x / 2;
        float halfSpriteHeight = spriteSize.y / 2;

        minPanLimit.x = -halfSpriteWidth * limitsMultiplier + cameraWidth / 2;
        maxPanLimit.x = halfSpriteWidth * limitsMultiplier - cameraWidth / 2;
        minPanLimit.y = -halfSpriteHeight * limitsMultiplier + cameraHeight / 2;
        maxPanLimit.y = halfSpriteHeight * limitsMultiplier - cameraHeight / 2;
    }
    
    void UpdateZoomState()
    {
        isZoomMin = cam.orthographicSize <= minZoom;
        isZoomMax = cam.orthographicSize >= maxZoom;        

        float scaleFactor = Mathf.Clamp(
            initialCamProjection / cam.orthographicSize, 
            1, 
            maxZoom / minZoom
        );

        zoomEvent.RequestZoomUpdated(scaleFactor);
    }

    Vector3 GetPointerScreenPosition()
    {
        if (isPinching && Touchscreen.current != null)
        {
            Vector2 center = GetPinchCenter();

            return new Vector3(center.x, center.y, 0f);
        }

        if (Mouse.current != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            return new Vector3(mousePosition.x, mousePosition.y, 0f);
        }

        return Vector3.zero;
    }

    Vector2 GetPinchCenter()
    {
        if (Touchscreen.current == null)
            return Vector2.zero;

        var touches = Touchscreen.current.touches;

        Vector2 first = Vector2.zero;
        Vector2 second = Vector2.zero;

        int count = 0;

        foreach (var touch in touches)
        {
            if (!touch.isInProgress) continue;

            if (count == 0)
            {
                first = touch.position.ReadValue();
            }
            else
            {
                second = touch.position.ReadValue();
                break;
            }

            count++;
        }

        return (first + second) / 2f;
    }

    public void ResetZoom()
    {
        cam.transform.position = initialCamPosition;
        cam.orthographicSize = initialCamProjection;

        previousZoom = initialCamProjection;

        UpdateZoomState();
    }
}