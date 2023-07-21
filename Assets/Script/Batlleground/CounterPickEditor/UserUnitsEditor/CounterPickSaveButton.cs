
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class CounterPickSaveButton: MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UserUnitEditorController userUnitEditorController;


        public void OnPointerClick(PointerEventData eventData)
        {
            userUnitEditorController.SaveCounterPick();
        }
    }
