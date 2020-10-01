using System;
using Factories;

namespace assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            factory.CreateMemberController(factory).mainNav();
        }
    }
}
