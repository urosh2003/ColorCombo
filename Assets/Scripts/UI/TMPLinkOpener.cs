using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TMPLinkOpener : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text textMeshPro;
    private Camera uiCamera;

    private void Awake()
    {
        uiCamera = Camera.main;
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, eventData.position, null);
        Debug.Log("Link Index: " + linkIndex);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            string linkId = linkInfo.GetLinkID();
            Application.OpenURL(linkId);
        }
    }
}