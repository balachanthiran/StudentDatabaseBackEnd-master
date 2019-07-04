using StudentDatabaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDatabaseAPI.DAL
{
    interface IUserRepository
    {
        List<User> GetUsers(int amount, string sort);

        User GetSingleUser(int userId);

        List<SearchResult> SearchUsers(string[] searchWords);

        List<Skill> GetAllSkillNames();

        int AddSkill(Skill newSkill);

        bool DeleteSkill(int userID, int skillID);

        bool UpdateSkill(int userID, UpdateSkill newValue);

        int AddLanguage(Language newLanguage);

        bool DeleteLanguage(int userID, int languageID);

        bool UpdateLanguage(int userID, UpdateLanguage newValue);

        AdditionalInfo getUserInfo(int userID);

        bool InsertUser(User ourUser);

        bool DeleteUser(int userId);

        bool UpdateUser(User ourUser);

        int AuthenticateUser(string email, string password);

        bool InsertAdditionalInfo(AdditionalInfo info);

        bool UpdateAdditionalInfo(AdditionalInfo info);

        bool InsertImage(string imageURL, int userid);

        bool UpdateEducation(int userID, UpdateEducation newEducation);

        bool UpdateSocial(int userID, UpdateSocial newSocial);
    }
}
