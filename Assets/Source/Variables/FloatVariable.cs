using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public float InitialValue;

    private float _currentValue;
    public float Value
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }

    private void OnEnable()
    {
        _currentValue = InitialValue;
    }

    public void SetValue(float value)
    {
        _currentValue = value;
    }

    public void SetValue(FloatVariable value)
    {
        _currentValue = value.Value;
    }

    public void ApplyChange(float amount)
    {
        _currentValue += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        _currentValue += amount.Value;
    }
}
