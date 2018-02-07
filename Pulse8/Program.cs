using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulse8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter MemberID: ");
            String memberIDStr = Console.ReadLine();
            int memberID;

            try
            {
                memberID = Convert.ToInt16(memberIDStr);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a valid Member ID.\nPress any key to continue...");
                Console.ReadLine();
                return;
            }

            Pulse8TestDBEntities entities = new Pulse8TestDBEntities();
            Member member = entities.Members.Where(x => x.MemberID == memberID).FirstOrDefault();

            if(member == null)
            {
                Console.WriteLine("Please enter a valid Member ID.\nPress any key to continue...");
                Console.ReadLine();
                return;
            }

            Diagnosi mostSevere = member.Diagnosis.OrderBy(x => x.DiagnosisID).FirstOrDefault();

            Console.WriteLine("Member ID: " + member.MemberID);
            Console.WriteLine("Name: " + member.FirstName + " " + member.LastName);

            if (mostSevere != null)
            {
                IEnumerable<DiagnosisCategory> categories = member.Diagnosis.SelectMany(x => x.DiagnosisCategories).Distinct();
                DiagnosisCategory mostSevereCategory = categories.OrderBy(x => x.DiagnosisCategoryID).FirstOrDefault();
                
                Console.WriteLine("Most Severe Diagnosis ID: " + mostSevere.DiagnosisID);
                Console.WriteLine("Most Severe Diagnosis Description: " + mostSevere.DiagnosisDescription);
                Console.WriteLine("Categories:");

                foreach (DiagnosisCategory category in categories)
                {
                    Console.WriteLine("\tID: " + category.DiagnosisCategoryID);
                    Console.WriteLine("\tDescription: " + category.CategoryDescription);
                    Console.WriteLine("\tScore: " + category.CategoryScore);

                    if (category.DiagnosisCategoryID == mostSevereCategory.DiagnosisCategoryID)
                    {
                        Console.WriteLine("\t\tMOST SEVERE");
                    }
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Member has no diagnoses.\nPress any key to continue...");
                Console.ReadLine();
            }
        }
    }
}
