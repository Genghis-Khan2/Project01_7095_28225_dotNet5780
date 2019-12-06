using System;

namespace UI
{
    class Program
    {

        public static void handleGuest()
        {
            //TODO: Implement the 3 functions
        }

        public static void handleHost()
        {

        }

        public static void handleAdmin()
        {

        }
        static void Main(string[] args)
        {
            bool isValidNum = false;
            string user;
            int user_i = 0;
            while (!isValidNum)
            {
                Console.WriteLine("Hello! Who are you?");
                Console.WriteLine("1. Guest\n2. Host\n3. Site admin");
                user = Console.ReadLine();
                isValidNum = int.TryParse(user, out user_i);
                if (isValidNum)
                {
                    if (user_i > 3 || user_i < 1)
                    {
                        isValidNum = false;
                    }
                }
                if (!isValidNum)
                {
                    Console.WriteLine("Invalid value. Please enter 1, 2 or 3");
                }
            }

            switch (user_i)
            {
                case 1:
                    handleGuest();
                    break;
                case 2:
                    handleHost();
                    break;
                case 3:
                    handleAdmin();
                    break;
            }
        }
    }
}
