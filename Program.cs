using System;

namespace assignment2 {
    class Program {
        static void Main(string[] args) {
            
            Boat boat1 = new Boat(Type.Canoe, 5);
            Console.WriteLine(boat1.Length);
            Console.WriteLine(boat1.Type);

        }
    }
}
