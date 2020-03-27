using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DynamicArray<string> arr = new DynamicArray<string>();
                bool isExit = false;
                while (!isExit)
                {
                    Console.Write('>');
                    string[] cmdArr = Console.ReadLine().Split(' ');
                    if (cmdArr.Length > 0)
                    {
                        switch (cmdArr[0])
                        {
                            case "add":
                                if (cmdArr.Length > 1) arr.Add(cmdArr[1]);
                                else Console.WriteLine("Не верный синтаксис");
                                break;
                            case "addRange":
                                if (cmdArr.Length > 1) arr.AddRange(cmdArr[1].Split(';'));
                                else Console.WriteLine("Не верный синтаксис");
                                break;
                            case "insert":
                                if (cmdArr.Length > 2) arr.Insert(int.Parse(cmdArr[1]), cmdArr[2]);
                                else Console.WriteLine("Не верный синтаксис");
                                break;
                            case "remove":
                                if (cmdArr.Length > 1) arr.Remove(cmdArr[1]);
                                else Console.WriteLine("Не верный синтаксис");
                                break;
                            case "exit":
                                isExit = true;
                                break;
                            default:
                                Console.WriteLine("Не верная команда!");
                                break;
                        }
                        foreach (var item in arr)
                            Console.WriteLine(item);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }
    }
}
