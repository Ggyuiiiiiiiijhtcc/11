﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Group
{
    private List<Student> students;

    public string Name { get; set; }
    public string Specialization { get; set; }
    public int Course { get; set; }

    public Student this[int index]
    {
        get
        {
            if (index < 0 || index >= students.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за пределы списка студентов");
            }
            return students[index];
        }
    }


    public Group()
    {
        students = new List<Student>();
    }

    public Group(Student[] studentsArray)
    {
        students = studentsArray.ToList();
    }

    public Group(List<Student> studentsList)
    {
        students = new List<Student>(studentsList);
    }

    public Group(Group otherGroup)
    {
        Name = otherGroup.Name;
        Specialization = otherGroup.Specialization;
        Course = otherGroup.Course;
        students = new List<Student>(otherGroup.students);
    }

    public void ShowAllStudents()
    {
        Console.WriteLine($"{Name} ({Specialization}), {Course} курс:");
        var orderedStudents = students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
        for (int i = 0; i < orderedStudents.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {orderedStudents[i]}");
        }
    }

    public void AddStudent(Student student)
    {
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Студент не может быть null");
        }
        students.Add(student);
    }

    public void EditStudent(Student oldStudent, Student newStudent)
    {
        if (oldStudent == null)
        {
            throw new ArgumentNullException(nameof(oldStudent), "Старый студент не может быть null");
        }
        if (newStudent == null)
        {
            throw new ArgumentNullException(nameof(newStudent), "Новый студент не может быть null");
        }
        var index = students.IndexOf(oldStudent);
        if (index < 0)
        {
            throw new ArgumentException("Студент не найден в группе", nameof(oldStudent));
        }
        students[index] = newStudent;
    }

    public void TransferStudent(Student student, Group newGroup)
    {
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Студент не может быть null");
        }
        if (newGroup == null)
        {
            throw new ArgumentNullException(nameof(newGroup), "Новая группа не может быть null");
        }
        if (!students.Contains(student))
        {
            throw new ArgumentException("Студент не найден в группе", nameof(student));
        }
        students.Remove(student);
        newGroup.AddStudent(student);
    }

    public void ExpelAllFailedStudents()
    {
        students.RemoveAll(s => !s.PassedSession);
    }
    public class Student
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Age { get; init; }
        public bool PassedSession { get; set; }
        public Dictionary<string, int> Grades { get; } = new Dictionary<string, int>();

        public double AverageGrade
        {
            get
            {
                if (Grades.Count == 0)
                {
                    return 0.0;
                }
                var sum = Grades.Sum(g => g.Value);
                return (double)sum / Grades.Count;
            }
        }

        public Student(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public void AddGrade(string subject, int grade)
        {
            Grades[subject] = grade;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}, возраст: {Age}, средний балл: {AverageGrade:0.00}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Student)obj;
            return AverageGrade == other.AverageGrade;
        }

        public override int GetHashCode()
        {
            return AverageGrade.GetHashCode();
        }

        public static bool operator ==(Student student1, Student student2)
        {
            if (ReferenceEquals(student1, student2))
            {
                return true;
            }

            if (student1 is null || student2 is null)
            {
                return false;
            }

            return student1.Equals(student2);
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return !(student1 == student2);
        }

        public static bool operator >(Student student1, Student student2)
        {
            if (student1 is null)
            {
                return false;
            }

            return student1.AverageGrade > student2?.AverageGrade;
        }

        public static bool operator <(Student student1, Student student2)
        {
            if (student2 is null)
            {
                return false;
            }

            return student1?.AverageGrade < student2.AverageGrade;
        }

        public static bool operator >=(Student student1, Student student2)
        {
            return student1 > student2 || student1 == student2;
        }

        public static bool operator <=(Student student1, Student student2)
        {
            return student1 < student2 || student1 == student2;
        }
    }
    public void SetStudent(int index, Student student)
    {
        if (index < 0 || index >= students.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за пределы списка студентов");
        }
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Студент не может быть null");
        }
        students[index] = student;
    }
    static void Main(string[] args)
    {
        Group group = new Group();
        Student student1 = new Student("Иван", "Иванов", 20);
        Student student2 = new Student("Петр", "Петров", 21);
        group.AddStudent(student1);
        group.AddStudent(student2);

        Console.WriteLine(group[0]); // выводит "Иванов Иван, возраст: 20, средний балл: 0.00"
        Console.WriteLine(group[1]); // выводит "Петров Петр, возраст: 21, средний балл: 0.00"
        group.SetStudent(0, new Student("Иван", "Иванов", 22));


                    

    }
    public void RemoveStudentByIndex(int index)
    {
        if (index < 0 || index >= students.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за пределы списка студентов");
        }
        students.RemoveAt(index);
    }
}

