using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string StudentID { get; set; }
    public string FullName { get; set; }
    public float AverageScore { get; set; }
    public string Faculty { get; set; }

    public Student(string studentID, string fullName, float averageScore, string faculty)
    {
        StudentID = studentID;
        FullName = fullName;
        AverageScore = averageScore;
        Faculty = faculty;
    }

    public Student() { }

    public void Input()
    {
        Console.Write("Nhập MSSV: ");
        StudentID = Console.ReadLine();

        Console.Write("Nhập họ tên sinh viên: ");
        FullName = Console.ReadLine();

        float score;
        do
        {
            Console.Write("Nhập điểm trung bình (0 - 10): ");
        } while (!float.TryParse(Console.ReadLine(), out score) || score < 0 || score > 10);
        AverageScore = score;

        Console.Write("Nhập khoa: ");
        Faculty = Console.ReadLine();
    }

    public void Show()
    {
        Console.WriteLine($"{StudentID,-10} | {FullName,-20} | {Faculty,-15} | {AverageScore,5:F2}");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        List<Student> students = new List<Student>();
        int choice;

        do
        {
            Console.WriteLine("\n===== MENU QUẢN LÝ SINH VIÊN =====");
            Console.WriteLine("1 - Thêm sinh viên");
            Console.WriteLine("2 - Xuất danh sách sinh viên");
            Console.WriteLine("3 - Xuất SV thuộc khoa CNTT");
            Console.WriteLine("4 - Xuất SV có điểm TB >= 5");
            Console.WriteLine("5 - Sắp xếp SV theo điểm TB tăng dần");
            Console.WriteLine("6 - Xuất SV có điểm TB >= 5 và khoa CNTT");
            Console.WriteLine("7 - Xuất SV có điểm TB cao nhất trong khoa CNTT");
            Console.WriteLine("8 - Thống kê số lượng SV theo xếp loại");
            Console.WriteLine("0 - Thoát");
            Console.Write("Chọn chức năng: ");
            if (!int.TryParse(Console.ReadLine(), out choice)) choice = -1;

            switch (choice)
            {
                case 1: AddStudent(students); break;
                case 2: DisplayStudents(students); break;
                case 3: ShowCNTT(students); break;
                case 4: ShowAbove5(students); break;
                case 5: SortByScore(students); break;
                case 6: ShowCNTTAbove5(students); break;
                case 7: ShowTopCNTT(students); break;
                case 8: ShowStatistics(students); break;
                case 0: Console.WriteLine("Thoát chương trình."); break;
                default: Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại."); break;
            }

        } while (choice != 0);
    }

    static void AddStudent(List<Student> students)
    {
        Student s = new Student();
        s.Input();
        students.Add(s);
        Console.WriteLine("✅ Thêm sinh viên thành công!");
    }

    static void DisplayStudents(List<Student> students)
    {
        if (!students.Any()) { Console.WriteLine("Danh sách trống."); return; }
        Console.WriteLine($"\n{"MSSV",-10} | {"Họ tên",-20} | {"Khoa",-15} | {"ĐTB",5}");
        Console.WriteLine(new string('-', 55));
        foreach (var s in students) s.Show();
    }

    static void ShowCNTT(List<Student> students)
    {
        var result = students.Where(s => s.Faculty.ToUpper() == "CNTT");
        Console.WriteLine("\n=== Sinh viên khoa CNTT ===");
        DisplayStudents(result.ToList());
    }

    
    static void ShowAbove5(List<Student> students)
    {
        var result = students.Where(s => s.AverageScore >= 5);
        Console.WriteLine("\n=== Sinh viên có điểm TB >= 5 ===");
        DisplayStudents(result.ToList());
    }

   
    static void SortByScore(List<Student> students)
    {
        var result = students.OrderBy(s => s.AverageScore).ToList();
        Console.WriteLine("\n=== Sắp xếp theo điểm TB tăng dần ===");
        DisplayStudents(result);
    }

    
    static void ShowCNTTAbove5(List<Student> students)
    {
        var result = students.Where(s => s.AverageScore >= 5 && s.Faculty.ToUpper() == "CNTT");
        Console.WriteLine("\n=== SV khoa CNTT có điểm TB >= 5 ===");
        DisplayStudents(result.ToList());
    }

  
    static void ShowTopCNTT(List<Student> students)
    {
        var cntt = students.Where(s => s.Faculty.ToUpper() == "CNTT");
        if (!cntt.Any()) { Console.WriteLine("Không có SV khoa CNTT."); return; }
        float max = cntt.Max(s => s.AverageScore);
        var result = cntt.Where(s => s.AverageScore == max);
        Console.WriteLine("\n=== SV điểm TB cao nhất khoa CNTT ===");
        DisplayStudents(result.ToList());
    }


    static void ShowStatistics(List<Student> students)
    {
        var groups = students.GroupBy(s => GetRank(s.AverageScore))
                             .Select(g => new { Rank = g.Key, Count = g.Count() });
        Console.WriteLine("\n=== Thống kê xếp loại ===");
        foreach (var g in groups)
        {
            Console.WriteLine($"{g.Rank}: {g.Count} sinh viên");
        }
    }

    static string GetRank(float score)
    {
        if (score >= 9) return "Xuất sắc";
        if (score >= 8) return "Giỏi";
        if (score >= 7) return "Khá";
        if (score >= 5) return "Trung bình";
        if (score >= 4) return "Yếu";
        return "Kém";
    }
}
