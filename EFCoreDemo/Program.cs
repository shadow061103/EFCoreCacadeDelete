using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //AddData();
            //UpdateData();
            //RemoveData();
            Console.ReadKey();
        }

        private static void AddData()
        {
            using (var context = new EfcoretestContext())
            {
                var class1 = new Classes() { ClassId = 1, ClassName = "國文" };
                var class2 = new Classes() { ClassId = 2, ClassName = "英文" };
                var class3 = new Classes() { ClassId = 3, ClassName = "數學" };

                var stu1 = new Students() { StudentId = 1, Name = "Kuan" };
                var stu2 = new Students() { StudentId = 2, Name = "Jenny" };
                var stu3 = new Students() { StudentId = 3, Name = "Emily" };

                var relation1 = new StudentClassRelation() { Classes = class1, Students = stu1 };
                var relation2 = new StudentClassRelation() { Classes = class1, Students = stu2 };

                context.Classes.Add(class1);
                context.Classes.Add(class2);
                context.Classes.Add(class3);

                context.Students.Add(stu1);
                context.Students.Add(stu2);
                context.Students.Add(stu3);

                context.StudentClassRelations.Add(relation1);
                context.StudentClassRelations.Add(relation2);

                context.SaveChanges();

                Console.WriteLine("Add finish");
            }
        }

        private static void UpdateData()
        {
            using (var context = new EfcoretestContext())
            {
                var relation = context.StudentClassRelations
                    .AsNoTracking()
                    .Include(c => c.Classes)
                    .Include(c => c.Students)
                    .FirstOrDefault(c => c.ClassId == 1 && c.StudentId == 1);

                relation.CreateDate = DateTime.Now;

                context.Update(relation);
                context.SaveChanges();
            }
        }

        private static void RemoveData()
        {
            using (var context = new EfcoretestContext())
            {
                //1.only delete StudentClassRelations
                //var relation = context.StudentClassRelations
                //    .Include(c => c.Classes)
                //    .Include(c => c.Students)
                //    .FirstOrDefault();
                //context.Remove(relation);
                //context.SaveChanges();

                //2. delete Class include StudentClassRelations
                // With OnDelete is DeleteBehavior.ClientSetNull
                var relation1 = context.Classes
                    .Include(c => c.StudentClassRelations)
                    .ThenInclude(c => c.Students)
                    .FirstOrDefault();

                context.Remove(relation1);
                context.SaveChanges();

                //throw exception
                //The association between entity types 'Classes' and 'StudentClassRelation' has been severed but the relationship is either marked as 'Required' or is implicitly required because the foreign key is not nullable. If the dependent / child entity should be deleted when a required relationship is severed, then setup the relationship to use cascade deletes.  Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the key values.'

                //3.With OnDelete is DeleteBehavior.Cascade
                //Delete Class and StudentClassRelations but not Student
            }
        }
    }
}