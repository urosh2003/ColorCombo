using UnityEngine;
using UnityEngine.UI;

public class ChestArrow : MonoBehaviour
{
    public Transform target;
    public float edgeMargin = 0.95f;
    public float clampMargin = 20f;

    private Image arrow;
    private Camera mainCamera;

    [SerializeField] bool targetingChest1;

    void Start()
    {
        arrow = GetComponent<Image>();
        mainCamera = Camera.main;
        ChestSpawner.instance.ChestSpawned += TargetChest;
        TargetChest();
    }

    private void OnDestroy()
    {
        ChestSpawner.instance.ChestSpawned -= TargetChest;

    }

    void TargetChest()
    {
        if(targetingChest1)
        {
            target = ChestSpawner.instance.chest1;
        }
        else
        {
            target = ChestSpawner.instance.chest2;
        }
    }

    void Update()
    {
        if (target == null)
        {
            TargetChest();
            return;
        }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        bool isOnScreen = IsTargetOnScreen(screenPos);

        arrow.enabled = !isOnScreen;
        arrow.transform.GetChild(0).gameObject.SetActive(!isOnScreen);
        if (!isOnScreen)
            PositionArrow(screenPos);
    }

    bool IsTargetOnScreen(Vector3 screenPos)
    {
        return screenPos.z > 0 &&
               screenPos.x > 0 && screenPos.x < Screen.width &&
               screenPos.y > 0 && screenPos.y < Screen.height;
    }

    void PositionArrow(Vector3 screenPos)
    {
        Vector3 playerPosition = PlayerManager.instance.transform.position;
        playerPosition = mainCamera.WorldToScreenPoint(playerPosition);
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 dir = (screenPos - playerPosition).normalized;
        // Rotate arrow to face target
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        arrow.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -angle);

        // Position
        Vector3 arrowPos = screenPos;

        // Clamp to screen
        arrowPos.x = Mathf.Clamp(arrowPos.x, clampMargin, Screen.width - clampMargin);
        arrowPos.y = Mathf.Clamp(arrowPos.y, clampMargin, Screen.height - clampMargin);
        arrowPos.z = 0;

        arrow.transform.position = arrowPos;

        
    }
}