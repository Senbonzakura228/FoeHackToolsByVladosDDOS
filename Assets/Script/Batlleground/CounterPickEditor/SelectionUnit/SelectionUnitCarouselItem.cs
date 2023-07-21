
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class SelectionUnitCarouselItem: MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private string unitType;
        
        public void SetUnit(string unitType, Sprite sprite)
        {
            this.unitType = unitType;
            SetIcon(sprite);
        }
        private void SetIcon(Sprite sprite)
        {
            this.icon.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var selectionUnitController = gameObject.GetComponentInParent<SelectionUnitController>();
            selectionUnitController.SelectUnit(unitType);
        }
    }
