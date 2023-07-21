using TMPro;
using UnityEngine;

public class AttritionController: MonoBehaviour
{
    public int value { get; private set; }
    [SerializeField] private TextMeshProUGUI text;

    public void SetValue(int value)
    {
        this.value = value;
        text.text = $"{"Attrition" + " " + this.value.ToString()}";
    }
}