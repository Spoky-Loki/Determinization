using System;
using System.Collections.Generic;

namespace determinization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var states = functions.readFromFile("NKA.txt");

            var newStates = functions.determinization(states);

            foreach (var st in newStates)
                System.Console.WriteLine(st.ToString());

            functions.createFileWithTable("NewNKA.txt", newStates);
        }
    }
}
