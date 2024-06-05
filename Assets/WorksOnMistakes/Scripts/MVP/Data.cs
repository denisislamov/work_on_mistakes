using UnityEngine;

[CreateAssetMenu(menuName = "Data/Data", fileName = "Data", order = 0)]
public class Data : ScriptableObject 
{
    private int _value;
    [SerializeField] private int _multiplier = 1;
    
    public int Value
    {
        get => _value;
        set => _value = value;
    }
    
    public int Multiplier => _multiplier;
}