namespace CheckTuring
{
    internal class Program
    {
        static void Main()
        {
            var alphabet = new List<string>();
            var states = new Dictionary<string, string[]>();

            Console.Write("Введите количество строк с описанием Машины Тьюринга: ");
            int m = int.Parse(Console.ReadLine());

            Console.Write("Введите разделитель для ленты(не пробел): ");
            char del = Console.ReadLine().First();


            Console.WriteLine($"Введите {m} строк вида: текущее состояние, входной символ -> новое состояние, выходной символ, смещение " +
                $"(q1 a -> q2 b +1)");

            for(var i = 0; i < m; i++)
            {
                var s = Console.ReadLine();
                var ss = s.Split(' ');


                alphabet.Add(ss[1]);
                alphabet.Add(ss[4]);

                states.Add(ss[0], new string[] { ss[1], ss[2], ss[3], ss[4], ss[5] });

            }

            Console.Write("Введите длину ленты: ");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите данные на ленте: ");
            string data = Console.ReadLine();

            if (data.Length > n)
               throw new Exception("Данные на ленте не могут превышать её длину.");

            foreach (var c in data)
                if (!alphabet.Contains(c.ToString()))
                    throw new Exception("Алфавит не содержит данных, указанных в строке.");

            data += new string(del, n - data.Length);
            alphabet = alphabet.Distinct().ToList();

            Console.Write("Введите начальное состояние: ");
            string start_st = Console.ReadLine();

            Console.Write("Введите конечное состояние: ");
            string last_st = Console.ReadLine();

            string[] check_st = { "Начальное состояние отсутсвует в словаре состояний.",
                                  "Конечное состояние отсутсвтует в словаре состояний.",
                                  start_st, last_st};

            for (var i = 0; i < 2; i++)
                if (!states.Keys.Contains(check_st[3 - i]))
                    throw new Exception(check_st[i]);
        }
    }
}