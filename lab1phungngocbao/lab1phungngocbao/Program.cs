using System;

namespace Lab1_Phung_Ngoc_Bao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Chuong trinh doan so (3 chu so) ===");

            Random rnd = new Random();
            int targetNumber = rnd.Next(100, 1000);
            string target = targetNumber.ToString();

            const int MAX_GUESS = 7;
            int attempt = 1;
            string feedback = "";
            bool won = false;

            // Debug (bỏ // để test nhanh)
            // Console.WriteLine("(DEBUG) So bi mat: " + target);

            while (attempt <= MAX_GUESS && !won)
            {
                Console.Write($"Lan doan thu {attempt}: ");
                string guess = Console.ReadLine()?.Trim();

                if (guess == null || guess.Length != 3 || !int.TryParse(guess, out _))
                {
                    Console.WriteLine("Nhap so hop le (3 chu so)!");
                    continue;
                }

                feedback = GetFeedback(target, guess);
                Console.WriteLine("Phan hoi tu may tinh: " + (feedback == "" ? "(khong co)" : feedback));

                if (feedback == "+++")
                {
                    Console.WriteLine($"Nguoi choi da doan dung sau {attempt} lan. Nguoi choi thang cuoc!");
                    won = true;
                }
                attempt++;
            }

            if (!won)
            {
                Console.WriteLine($"Nguoi choi da doan {MAX_GUESS} lan. Tro choi ket thuc!");
                Console.WriteLine("Nguoi choi thua cuoc. So can doan la: " + target);
            }

            Console.WriteLine("Nhan Enter de thoat...");
            Console.ReadLine();
        }

        static string GetFeedback(string target, string guess)
        {
            char[] result = new char[3];
            bool[] usedTarget = new bool[3];
            bool[] usedGuess = new bool[3];

            // Bước 1: tìm '+'
            for (int i = 0; i < 3; i++)
            {
                if (guess[i] == target[i])
                {
                    result[i] = '+';
                    usedTarget[i] = true;
                    usedGuess[i] = true;
                }
            }

            // Bước 2: tìm '?'
            for (int i = 0; i < 3; i++)
            {
                if (usedGuess[i]) continue;
                for (int j = 0; j < 3; j++)
                {
                    if (!usedTarget[j] && guess[i] == target[j])
                    {
                        result[i] = '?';
                        usedTarget[j] = true;
                        usedGuess[i] = true;
                        break;
                    }
                }
            }

            return new string(result).Replace("\0", "");
        }
    }
}