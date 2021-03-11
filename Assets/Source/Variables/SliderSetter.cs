using UnityEngine;
using UnityEngine.UI;

public class SliderSetter : MonoBehaviour
{
    public Slider Slider;
    public FloatVariable Variable;

    private void OnEnable()
    {
        if (Slider != null && Variable != null)
            Slider.maxValue = Variable.InitialValue;
    }

    private void Update()
    {
        if (Slider != null && Variable != null)
            Slider.value = Variable.Value;
    }
}
