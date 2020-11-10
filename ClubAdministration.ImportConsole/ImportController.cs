using ClubAdministration.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ClubAdministration.ImportConsole
{
  public class ImportController
  {
    const string FileName = "members.csv";

        public static async Task<MemberSection[]> ReadFromCsvAsync()
        {
            string[][] csvFile = await MyFile
                .ReadStringMatrixFromCsvAsync(FileName, false);

            List <Section> sections = csvFile
                .GroupBy(sm => sm[2])
                .Select(sm => new Section { Name = sm.Key })
                .ToList();

            //var lastNames = csvFile.Select(m => m[0]).Distinct().ToArray();
            //var firstNames = csvFile.Select(m => m[1]).Distinct().ToArray();

            List<Member> members = csvFile
                .GroupBy(m => m[0])
                .Select(member => new Member
                {
                    LastName = member.Key,
                    FirstName = csvFile.Select(s => s[1]).FirstOrDefault()
                })
                .ToList();

            MemberSection[] memberSections = csvFile
                .Select(ms => new MemberSection
                {
                    Section = sections.Where(s => s.Name.Equals(ms[2])).FirstOrDefault(),
                    Member = members.Where(m => m.LastName.Equals(ms[0])).FirstOrDefault()
                })
                .ToArray()
                ;
            return memberSections;
        }
  }
}
