using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserUnitCarouselItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] public int id;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color activeColor;
    public string unitType;

    public void SetUnit(string unitType, Sprite sprite)
    {
        this.unitType = unitType;
        SetIcon(sprite);
        text.text = "";
        icon.color = activeColor;
    }
    
    private void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var userUnitEditorController = gameObject.GetComponentInParent<UserUnitEditorController>();
        userUnitEditorController.OpenUnitSelectionDialog(id);
    }
}