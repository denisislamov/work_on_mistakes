using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


public class ArraysCalculationSample : MonoBehaviour
{
    private class /*struct*/ IntWrapper
    {
        public int Value;
    }
    
    [BurstCompile]
    private struct SummCollectionJob : IJob
    {
        [ReadOnly] public NativeArray<int> values;
        private int _summ;
        public void Execute()
        {
            for (var i = 0; i < values.Length; i++)
            {
                _summ += values[i];
            }
        }
    }

    
    [SerializeField] private int count = 100;
    
    private int[] _numbers;
    private List<int> _numbersList;
    private LinkedList<int> _numbersLinkedList;
    
    private IntWrapper[] _numbersStruct;
    private List<IntWrapper> _numbersListStruct;
    private LinkedList<IntWrapper> _numbersLinkedListStruct;
    
    private readonly Dictionary<KeyCode, Action> _calculationActions = new();

    private void Awake()
    {
        _calculationActions.Add(KeyCode.Alpha1, ForSampleInt);
        _calculationActions.Add(KeyCode.Alpha2, ForeachSampleInt);
        _calculationActions.Add(KeyCode.Alpha3, ForeachSampleLinq);
        _calculationActions.Add(KeyCode.Alpha4, ForSampleIntWrapper);
        _calculationActions.Add(KeyCode.Alpha5, JobSample);
    }

    private void Update()
    {
        if (!Input.anyKeyDown)
        {
            return;
        }
        
        foreach (var action in _calculationActions)
        {
            if (Input.GetKeyDown(action.Key))
            {
                action.Value.Invoke();
            }
        }
    }
    
    private void InitCollections<T>(out T[] numbers, out List<T> numbersList, out LinkedList<T> numbersLinkedList, T value = default)
    {
        numbers = new T[count];
        numbersList = new List<T>(count);
        numbersLinkedList = new LinkedList<T>();
        
        for (int i = 0; i < count; i++)
        {
            numbers[i] = value;
            numbersList.Add(value);
            numbersLinkedList.AddLast(value);
        }
    }

    [ContextMenu("ForSampleInt")]
    public void ForSampleInt()
    {
        InitCollections(out _numbers, out _numbersList, out _numbersLinkedList, 1);

        var summ = 0;
        var startTime = DateTime.Now;
        
        for (int i = 0; i < count; i++)
        {
            summ += _numbers[i];
        }
        var timeForArray = DateTime.Now - startTime;

        summ = 0;
        startTime = DateTime.Now;
       
        for (int i = 0; i < count; i++)
        {
            summ += _numbersList[i];
        }
        var timeForList = DateTime.Now - startTime;
        
        summ = 0;
        startTime = DateTime.Now;
        
        var node = _numbersLinkedList.First;
        while (node != null)
        {
            summ += node.Value;
            node = node.Next;
        }
        var timeForLinkedList = DateTime.Now - startTime;
        
        TimeDebugLog("ForSampleInt", timeForArray, timeForList, timeForLinkedList);
    }

    [ContextMenu("ForeachSampleInt")]
    public void ForeachSampleInt()
    {
        InitCollections(out _numbers, out _numbersList, out _numbersLinkedList, 1);
        
        var summ = 0;
        var startTime = DateTime.Now;

        foreach (var number in _numbers)
        {
            summ += number;
        }
        var timeForArray = DateTime.Now - startTime;

        summ = 0;
        startTime = DateTime.Now;
        
        foreach (var number in _numbersList)
        {
            summ += number;
        }
        var timeForList = DateTime.Now - startTime;
        
        summ = 0;
        startTime = DateTime.Now;
        
        foreach (var number in _numbersLinkedList)
        {
            summ += number;
        }
        var timeForLinkedList = DateTime.Now - startTime;
        
        TimeDebugLog("ForeachSampleInt", timeForArray, timeForList, timeForLinkedList);
    }
    
    [ContextMenu("ForeachSampleLinq")]
    public void ForeachSampleLinq()
    {
        InitCollections(out _numbers, out _numbersList, out _numbersLinkedList, 1);

        var startTime = DateTime.Now;
        _numbers.Sum();
        var timeForArray = DateTime.Now - startTime;

        startTime = DateTime.Now;
        _numbersList.Sum();
        var timeForList = DateTime.Now - startTime;

        startTime = DateTime.Now;
        _numbersLinkedList.Sum();
        var timeForLinkedList = DateTime.Now - startTime;
        
        TimeDebugLog("ForeachSampleLinq", timeForArray, timeForList, timeForLinkedList);
    }
    
    [ContextMenu("ForSampleIntWrapper")]
    public void ForSampleIntWrapper()
    {
        InitCollections(out _numbersStruct, out _numbersListStruct, out _numbersLinkedListStruct, 
            new IntWrapper {Value = 1});

        var summ = 0;
        var startTime = DateTime.Now;
        
        for (int i = 0; i < count; i++)
        {
            summ += _numbersStruct[i].Value;
        }
        var timeForArray = DateTime.Now - startTime;

        summ = 0;
        startTime = DateTime.Now;
       
        for (int i = 0; i < count; i++)
        {
            summ += _numbersListStruct[i].Value;
        }
        var timeForList = DateTime.Now - startTime;
        
        summ = 0;
        startTime = DateTime.Now;
        
        var node = _numbersLinkedListStruct.First;
        while (node != null)
        {
            summ += node.Value.Value;
            node = node.Next;
        }
        var timeForLinkedList = DateTime.Now - startTime;
        
        TimeDebugLog("ForSampleIntStruct", timeForArray, timeForList, timeForLinkedList);
    }
    
    public void JobSample()
    {
        var values = new NativeArray<int>(count, Allocator.TempJob);
        var job = new SummCollectionJob { values = values
        };
        
        var startTime = DateTime.Now;
        
        var handle = job.Schedule();
        handle.Complete();
        
        var resultTime = DateTime.Now - startTime;
        
        Debug.Log("JobSample time: " + resultTime.Ticks);
        
        values.Dispose();
    }
    
    private static void TimeDebugLog(string prefix, TimeSpan timeForArray, TimeSpan timeForList, TimeSpan timeForLinkedList)
    {
        Debug.Log(prefix + $"\nArray calculation time: {timeForArray.Ticks}\n" +
                        $"List calculation time: {timeForList.Ticks}\n" + 
                        $"LinkedList calculation time: {timeForLinkedList.Ticks}");
    }
}
