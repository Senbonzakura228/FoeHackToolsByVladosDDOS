using UnityEngine;
using UnityEngine.EventSystems;

public class OpenDialogPageButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform redirectedPage;
    private DialogController dialogController;

    private void Start()
    {
        dialogController = gameObject.GetComponentInParent<DialogController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        dialogController.OpenDialog(redirectedPage);
    }
}