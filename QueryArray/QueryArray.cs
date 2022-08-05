using System.Collections;

namespace QueryArray;

public class QueryArray<T> : IEnumerable<T>, IEnumerator<T>
{
    private T[] arr;
    private int arrSize;
    private int currentIndex;

    private bool eof; //end of file
    private bool bof; //begin of file

    private int capacity => arr.Length;

    private const int DefaultCapacity = 100;
    

    public QueryArray(int capacity)
    {
        arr = new T[capacity];
        // arrSize = capacity;
        Reset();
    }

    public QueryArray() : this(DefaultCapacity)
    {
        
    }

    void IEnumerator.Reset()
    {
        Reset();
    }

    object IEnumerator.Current => Current;

    public T Current
    {
        get
        {
            if (currentIndex == -1)
            {
                currentIndex = 0;
            }
            else if (currentIndex == arrSize)
            {
                currentIndex = arrSize - 1;
            }
            return arr[currentIndex];
        }
    }

    public bool MoveNext()
    {
        return Next();
    }

    private void Reset()
    {
        SetIndex(-1);
    }

    public T this[int index] => arr[index];

    public int Add(T item)
    {
        if (arrSize == capacity)
        {
            Resize(arrSize * 2);
        }

        arr[arrSize] = item;
        arrSize++;

        return arrSize;
    }

    public int AddRange(T[] items)
    {
        if (arrSize + items.Length >= capacity)
        {
            Resize(capacity * 2);
        }

        for (int i = 0; i < items.Length; i++)
        {
            arr[arrSize] = items[i];
            arrSize++;
        }

        return arrSize;
    }

    public void RemoveAt(int index)
    {
        for (int i = index; i < arrSize - 1; i++)
        {
            arr[i] = arr[i + 1];
        }

        arrSize--;
    }

    private void Resize(int newSize)
    {
        Array.Resize(ref arr, newSize);
    }
    private void Load(T[]newArray)
    {
        ArgumentNullException.ThrowIfNull(newArray);
        arr = newArray;
        arrSize = arr.Length;
        Reset();
    }

    public void LoadFromArray(T[] newArr)
    {
        Load(newArr);
    }

    private void SetCurrent(int increase)
    {
        currentIndex += increase;
        eof = currentIndex > 0 && currentIndex >= arrSize - 1;
        bof = currentIndex == 0;
    }

    private void SetIndex(int index)
    {
        currentIndex = 0;
        SetCurrent(index);
    }

    public void GotoStart()
    {
        Reset();
    }

    public void GotoEnd()
    {
        SetIndex(arrSize);
    }
    public bool Next()
    {
        if (!eof)
        {
            SetCurrent(1);
            return true;
        } 
        
        GotoEnd();
        return false;
    }
    
    public bool Previous()
    {
        if (!bof)
        {
            SetCurrent(-1);
            return true;
        }
        
        GotoStart();
        return false;
    }

    public static implicit operator QueryArray<T>(T[] newArr)
    {
        var q = new QueryArray<T>();
        q.LoadFromArray(newArr);
        return q;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(arr);
    }
}