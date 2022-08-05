// See https://aka.ms/new-console-template for more information

using QueryArray;
Console.WriteLine("Hello, World!");

var arr = new []{1,2,3,4,5,6,7,8,9};

QueryArray<int> queryArr = new QueryArray<int>();
// queryArr.Add(1);
// queryArr.Add(2);
// queryArr.Add(3);

queryArr.AddRange(new[] { 1, 2, 3, 4, 5 });

queryArr.RemoveAt(0);

foreach (var q in queryArr)
{
    Console.WriteLine("foreach: " + q);
}

// while (queryArr.Next())
// {
//     Console.WriteLine("NX: " + queryArr.Current);
// }
//
// while (queryArr.Previous())
// {
//     Console.WriteLine("PR: " + queryArr.Current);
// }
//
// Console.WriteLine("PR: " + queryArr.Current);