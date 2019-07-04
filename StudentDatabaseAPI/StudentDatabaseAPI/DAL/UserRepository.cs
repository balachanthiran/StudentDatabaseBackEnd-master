using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentDatabaseAPI.Models;
using Dapper;
using System.Data;

using System.Diagnostics.Tracing;
using System.Diagnostics;
using System.Data.SqlClient;
namespace StudentDatabaseAPI.DAL
{
    public class UserRepository : IUserRepository
    {
        private string connectionString;

        public UserRepository()
        {
            connectionString = "Data Source=studentdb2017.database.windows.net;Initial Catalog=StudentDB;Persist Security Info=True;User ID=studentdb;Password=Bachelor2017";

        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public int AuthenticateUser(string email, string password)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string authQuery = "SELECT userid FROM tbluser"
                                + " WHERE email = @Email AND password = @Password";
                dbConnection.Open();
                return dbConnection.Query<int>(authQuery, new { Email = email, Password = password }).FirstOrDefault();
            }
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
        public User GetSingleUser(int userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM tbluser us"
                                + " WHERE us.userid = @UserID";
                dbConnection.Open();
                return dbConnection.Query<User>(sQuery, new { UserID = userId }).FirstOrDefault();
            }
        }

        public List<User> GetUsers(int amount, string sort)
        {
            throw new NotImplementedException();
        }

        public bool InsertLanguages(Language languages)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO tbllanguage (userid, language, level)"
                                + " VALUES(@UserId, @Language, @Level)";
                dbConnection.Open();
                int rowsAffected = dbConnection.Execute(sQuery, languages);

                return (rowsAffected > 0);
            }
        }

        public bool InsertAdditionalInfo(AdditionalInfo info)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                int rowsAffected;
                string sQuery;

                foreach (var skill in info.Skills)
                {
                    sQuery = "INSERT INTO tblskill (userid, name, level)"
                                 + " VALUES(@UserId, @Name, @Level)";
                    rowsAffected = dbConnection.Execute(sQuery, skill);

                    if (rowsAffected == 0) return false;

                }

                foreach (var language in info.Languages)
                {
                    sQuery = "INSERT INTO tbllanguage (userid, name, level)"
                                 + " VALUES(@UserId, @Name, @Level)";
                    rowsAffected = dbConnection.Execute(sQuery, language);

                    if (rowsAffected == 0) return false;

                }

                    sQuery = "INSERT INTO tbleducation (userid, faculty, study)"
                     + " VALUES(@UserId, @Faculty, @Study)";
                    rowsAffected = dbConnection.Execute(sQuery, info.Education);

                    if (rowsAffected == 0) return false;


                    sQuery = "INSERT INTO tblsocial (userid, facebook)"
                     + " VALUES(@UserId, @Facebook)";
                    rowsAffected = dbConnection.Execute(sQuery, info.Social);

                    if (rowsAffected == 0) return false;


                    sQuery = "INSERT INTO tblsocial (userid, linkedin)"
                     + " VALUES(@UserId, @Linkedin)";
                    rowsAffected = dbConnection.Execute(sQuery, info.Social);

                    if (rowsAffected == 0) return false;


                return true;

            }
        }

        public bool InsertUser(User newUser)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO tbluser (firstname, lastname, email, password, gender, birthday, nationality, city)"
                                + " VALUES(@Firstname, @Lastname, @Email, @Password, @Gender, @Birthday, @Nationality, @City)";
                dbConnection.Open();
                int rowsAffected = dbConnection.Execute(sQuery, newUser);

                if (rowsAffected > 0) return true;

                return false;
            }
        }

        public bool UpdateUser(User ourUser)
        {
            throw new NotImplementedException();
        }

        public List<SearchResult> SearchUsers(string[] searchWords)
        {
            using (IDbConnection dbConnection = Connection)
            {

                List<SearchResult> searchResults = new List<SearchResult>();
                
                foreach (var word in searchWords)
                {
                    if (word != null)
                    {
                        string sQuery = "select us.userid, us.firstname, us.lastname, us.email, us.gender, us.birthday, us.city, us.nationality from tbluser us"
                          + " left join tblskill sk on us.userid = sk.userid"
                          + " left join tbllanguage la on us.userid = la.userid"
                          + " left join tbleducation ed on us.userid = ed.userid"
                          + " where us.firstname like @searchword"
                          + " OR"
                          + " us.lastname like @searchword"
                          + " OR"
                          + " us.gender like @searchword"
                          + " OR"
                          + " us.city like @searchword"
                          + " OR"
                          + " us.nationality like @searchword"
                          + " OR"
                          + " sk.name like @searchword"
                          + " OR"
                          + " la.name like @searchword"
                          + " OR"
                          + " ed.faculty like @searchword"
                          + " OR"
                          + " ed.study like @searchword";

                        long start = Environment.TickCount;

                        List<User> users = dbConnection.Query<User>(sQuery, new { searchword = "%" + word + "%"}).ToList();

                        long end = Environment.TickCount;
                        long result = end - start;
                        Debug.WriteLine("User query time: " + result/1000);

                        start = Environment.TickCount;
                        foreach (var user in users)
                        {
                            if (searchResults.Find(searchResult => searchResult.User == user) == null)
                            {

                                string additionalInfoQuery = @"
                                    SELECT study FROM tbleducation where userid = @userid
                                    
                                    SELECT name FROM tblskill where userid = @userid

                                    SELECT name FROM tbllanguage where userid = @userid";

                                var additionalInfoResult = dbConnection.QueryMultiple(additionalInfoQuery, new { userid = user.UserID });

                                searchResults.Add(new SearchResult(user, additionalInfoResult.Read<string>().FirstOrDefault(), additionalInfoResult.Read<string>().ToList(), additionalInfoResult.Read<string>().ToList()));
                            }

                        }
                        end = Environment.TickCount;
                        result = end - start;
                        Debug.WriteLine("Skills, language and education time: " + result / 1000);




                    }

                }
                return searchResults;
            }
        }

        public List<Skill> GetAllSkillNames()
        {
            using (IDbConnection dbConnection = Connection)
            {
                List<Skill> skillResult = new List<Skill>();

                string sQuery = "SELECT name FROM tblskill";

                skillResult = dbConnection.Query<Skill>(sQuery).ToList();

                return skillResult;
            }

        }

        public bool InsertImage(string imageURL, int userID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string imageQuery = "update tbluser set image = @imageurl where userid = @id";

                int rowsAffected = dbConnection.Execute(imageQuery, new { imageurl = imageURL, id = userID });

                if (rowsAffected > 0) return true;

                return false;

            }
        }

        public AdditionalInfo getUserInfo(int userID)
        {
                using (IDbConnection dbConnection = Connection)
            {

                string educationQuery = "SELECT * FROM tbleducation"
                                    + " where userid = @userid";

                Education education = dbConnection.Query<Education>(educationQuery, new { userid = userID }).FirstOrDefault();

                string socialQuery = "SELECT * FROM tblsocial"
                                    + " where userid = @userid";

                Social social = dbConnection.Query<Social>(socialQuery, new { userid = userID }).FirstOrDefault();

                string languageQuery = "SELECT * FROM tbllanguage"
                + " where userid = @userid";

                List<Language> languages = dbConnection.Query<Language>(languageQuery, new { userid = userID }).ToList();

                string skillQuery = "SELECT * FROM tblskill"
                    + " where userid = @userid";

                List<Skill> skills = dbConnection.Query<Skill>(skillQuery, new { userid = userID }).ToList();

                return new AdditionalInfo(education, social, languages, skills);

            }

        }

        public bool UpdateAdditionalInfo(AdditionalInfo info)
        {
            throw new NotImplementedException();
        }

        public int AddSkill(Skill newSkill)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string skillQuery = "INSERT INTO tblskill (userid, name, level)"
                                + " VALUES(@UserId, @Name, @Level)";
                int rowsAffected = dbConnection.Execute(skillQuery, newSkill);

                if (rowsAffected <= 0) return -1;

                string skillIdQuery = "SELECT skillid FROM tblskill WHERE userid = @userid and name = @name";

                return dbConnection.QuerySingle<int>(skillIdQuery, new { userid = newSkill.UserId, name = newSkill.Name });


            }
        }

        public bool DeleteSkill(int userID, int skillID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string imageQuery = "DELETE FROM tblskill WHERE userid = @userid AND skillid = @skillid";

                int rowsAffected = dbConnection.Execute(imageQuery, new { userid = userID, skillid = skillID });

                return (rowsAffected > 0);
            }
        }

        public bool UpdateSkill(int userID, UpdateSkill newValue)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string imageQuery = "UPDATE tblskill SET level = @level WHERE userid = @id AND skillid = @skillid";

                int rowsAffected = dbConnection.Execute(imageQuery, new { level = newValue.NewValue, id = userID, skillid = newValue.SkillID });

                return (rowsAffected > 0);

            }
        }

        public int AddLanguage(Language newLanguage)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string languageQuery = "INSERT INTO tbllanguage (userid, name, level)"
                                + " VALUES(@UserId, @Name, @Level)";
                int rowsAffected = dbConnection.Execute(languageQuery, newLanguage);

                if (rowsAffected <= 0) return -1;

                string languageIdQuery = "SELECT languageid FROM tbllanguage WHERE userid = @userid and name = @name";

                return dbConnection.QuerySingle<int>(languageIdQuery, new { userid = newLanguage.UserId, name = newLanguage.Name });


            }
        }

        public bool DeleteLanguage(int userID, int languageID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string languageQuery = "DELETE FROM tbllanguage WHERE userid = @userid AND languageid = @languageid";

                int rowsAffected = dbConnection.Execute(languageQuery, new { userid = userID, languageid = languageID });

                return (rowsAffected > 0);
            }
        }

        public bool UpdateLanguage(int userID, UpdateLanguage newValue)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string languageQuery = "UPDATE tbllanguage SET level = @level WHERE userid = @id AND languageid = @languageid";

                int rowsAffected = dbConnection.Execute(languageQuery, new { level = newValue.NewValue, id = userID, languageid = newValue.LanguageID });

                return (rowsAffected > 0);

            }
        }

        public bool UpdateEducation(int userID, UpdateEducation newEducation)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string educationQuery = "UPDATE tbleducation SET faculty = @faculty, study = @study WHERE userid = @id";

                int rowsAffected = dbConnection.Execute(educationQuery, new { faculty = newEducation.Faculty, study = newEducation.Study, id = userID});

                return (rowsAffected > 0);

            }
        }

        public bool UpdateSocial(int userID, UpdateSocial newSocial)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string socialQuery = "UPDATE tblsocial SET facebook = @facebook, linkedin = @linkedin WHERE userid = @id";

                int rowsAffected = dbConnection.Execute(socialQuery, new { facebook = newSocial.Facebook, linkedin = newSocial.LinkedIn, id = userID });

                return (rowsAffected > 0);

            }
        }
    }
}
