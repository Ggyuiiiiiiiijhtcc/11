using System;
using System.Collections.Generic;
using System.Linq;
using static Group;

public class Person
{
    public int Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public string EyeColor { get; set; }
    // Другие свойства, общие для всех людей
}

public class Student : Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
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
}

public class Aspirant : Student
{
    public string ThesisTopic { get; set; }

    public Aspirant(string firstName, string lastName, int age, string thesisTopic)
        : base(firstName, lastName, age)
    {
        ThesisTopic = thesisTopic;
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName}, возраст: {Age}, средний балл: {AverageGrade:0.00}, тема диссертации: {ThesisTopic}";
    }
}
public class Group
{
    private List<Student> students;
    public string Name { get; set; }
    public string Specialization { get; set; }
    public int Course { get; set; }

    public Person this[int index]
    {
        get
        {
            if (index < 0 || index >= persons.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за пределы списка студентов");
            }
            return persons[index];
        }
    }


    public Group()
    {
        persons = new List<Person>();
    }

    public Group(Person[] personsArray)
    {
        persons = personsArray.ToList();
    }

    public Group(List<Person> personsList)
    {
        persons = new List<Person>(personsList);
    }

    public Group(Group otherGroup)
    {
        Name = otherGroup.Name;
        Specialization = otherGroup.Specialization;
        Course = otherGroup.Course;
        persons = new List<Person>(otherGroup.persons);
    }

    public void ShowAllPersons()
    {
        Console.WriteLine($"{Name} ({Specialization}), {Course} курс:");
        var orderedPersons = persons.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
        for (int i = 0; i < orderedPersons.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {orderedPersons[i]}");
        }
    }

    public void AddPerson(Person person)
    {
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person), "Персона не может быть null");
        }
        persons.Add(person);
    }

    public void EditPerson(Person oldPerson, Person newPerson)
    {
        if (oldPerson == null)
        {
            throw new ArgumentNullException(nameof(oldPerson), "Старая персона не может быть null");
        }
        if (newPerson == null)
        {
            throw new ArgumentNullException(nameof(newPerson), "Новая персона не может быть null");
        }
        var index = persons.IndexOf(oldPerson);
        if (index < 0)
        {
            throw new ArgumentException("Персона не найдена в группе", nameof(oldPerson));
        }
        persons[index] = newPerson;
    }

    public void TransferPerson(Person person, Group newGroup)
    {
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person), "Персона не может быть null");
        }
        if (newGroup == null)
        {
            throw new ArgumentNullException(nameof(newGroup), "Новая группа не может быть null");
        }
        if (!persons.Contains(person))
        {
            throw new ArgumentException("Персона не найдена в группе", nameof(person));
        }
        persons.Remove(person);
        newGroup.AddPerson(person);
    }

    public void ExpelAllFailedPersons()
    {
        persons.RemoveAll(p => p is Student && !((Student)p).PassedSession);
    }

    public class Person
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Age { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string EyeColor { get; init; }

        public Person(string firstName, string lastName, int age, double height, double weight, string eyeColor)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Height = height;
            Weight = weight;
            EyeColor = eyeColor;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Height: {Height}");
            Console.WriteLine($"Weight: {Weight}");
            Console.WriteLine($"Eye Color: {EyeColor}");
        }
    }
}