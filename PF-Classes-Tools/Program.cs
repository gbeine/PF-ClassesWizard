using System;
using System.Collections.Generic;
using PF_Classes.Identifier;

namespace PF_Classes_Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Dictionary<String, String> dict = new Dictionary<string, string>();
            foreach (var identifier in Features.INSTANCE.AllIdentifiers)
            {
                if (dict.ContainsKey(identifier.Value))
                {
                    String first = dict[identifier.Value];
                    Console.WriteLine($"Duplicate for {identifier.Key}, {first}");
                }
                else
                {
                    dict[identifier.Value] = identifier.Key;
                }
            }
        }
    }
}