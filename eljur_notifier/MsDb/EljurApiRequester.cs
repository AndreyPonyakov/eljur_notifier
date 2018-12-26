using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eljur_notifier.MsDbNS
{
    class EljurApiRequester
    {
        internal protected Message message { get; set; }
        internal protected JObject rules { get; set; }
        internal protected String clas { get; set; }
        internal protected int eljurAccountId { get; set; }
        internal protected TimeSpan startTimeLessons { get; set; }
        internal protected TimeSpan endTimeLessons { get; set; }

        public EljurApiRequester()
        {
            this.message = new Message();
        }


        public TimeSpan getStartTimeLessonsByFullFIO(String FullFIO)
        {
            Random random = new Random();
            TimeSpan start = TimeSpan.FromHours(7);
            TimeSpan end = TimeSpan.FromHours(11);
            int maxMinutes = (int)((end - start).TotalMinutes);
            int minutes = random.Next(maxMinutes);
            TimeSpan t = start.Add(TimeSpan.FromMinutes(minutes));
            return t;
        }

        public TimeSpan getEndTimeLessonsByFullFIO(String FullFIO)
        {
            Random random = new Random();
            TimeSpan start = TimeSpan.FromHours(12);
            TimeSpan end = TimeSpan.FromHours(18);
            int maxMinutes = (int)((end - start).TotalMinutes);
            int minutes = random.Next(maxMinutes);
            TimeSpan t = start.Add(TimeSpan.FromMinutes(minutes));
            return t;
        }

        public int getEljurAccountIdByFullFIO(String FullFIO)
        {
            Random random = new Random();
            int eljurAccountId = random.Next(1, 32765);
            //message.Display(eljurAccountId, "Trace");
            return eljurAccountId;
        }

        public String getClasByFullFIO(String FullFIO)
        {
            Random random = new Random();
            String clas = random.Next(1, 11).ToString() + RandomString(1);
            //message.Display(clas, "Trace");
            return clas;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "АБВГ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public JObject geRulesDataAboutUser()
        {
            dynamic rules = JsonConvert.DeserializeObject(jsonStrRulesExample);
            var response = rules.response;
            var state = response.state;
            var error = response.error;
            var result = response.result;
            var allowedAds = response.allowedAds;
            var allowedSections = response.allowedSections;
            var name = response.name;
            var title = response.title;
            var lastname = response.lastname;
            var firstname = response.firstname;
            var middlename = response.middlename;
            var gender = response.gender;
            var email = response.email;
            var region = response.region;
            var city = response.city;

            message.Display(rules.GetType().ToString(), "Trace");
            foreach (var item in result)
            {
                message.Display(item.ToString(), "Trace");
            }
            return rules; 
        }

        String jsonStrRulesExample = @"{
        ""response"": {
            ""state"": 200,
            ""error"": null,
            ""result"": {
              ""roles"": [
                ""parent""
              ],
              ""relations"": {
                ""students"": {
                  ""386"": {
                    ""rules"": [
                      ""getassessments"",
                      ""getdiary"",
                      ""getfinalassessments"",
                      ""gethomework"",
                      ""getperiods"",
                      ""getschedule""
                    ],
                    ""rel"": ""child"",
                    ""name"": ""386"",
                    ""title"": ""Дмитриев Евгений"",
                    ""lastname"": ""Дмитриев"",
                    ""firstname"": ""Евгений"",
                    ""gender"": ""male"",
                    ""class"": ""10А"",
                    ""parallel"": ""10"",
                    ""city"": ""ЭлЖур""
                  }
                },
                ""groups"": {
                  ""10А"": {
                    ""rules"": [
                      ""getschedule"",
                      ""gethomework""
                    ],
                    ""rel"": ""homeclass"",
                    ""name"": ""10А"",
                    ""parallel"": ""10"",
                    ""balls"": 5
                  }
                }
              },
              ""allowedAds"": [
                ""eljur_menu"",
                ""admob""
              ],
              ""allowedSections"": [
                ""diary"",
                ""marks"",
                ""finalmarks"",
                ""schedule"",
                ""messages"",
                ""updates""
              ],
              ""name"": ""1011"",
              ""title"": ""Терехина Полина Михайловна"",
              ""lastname"": ""Терехина"",
              ""firstname"": ""Полина"",
              ""middlename"": ""Михайловна"",
              ""gender"": ""female"",
              ""email"": ""api+schooltest1011@eljur.ru"",
              ""region"": ""Московская область"",
              ""city"": ""ЭлЖур""
            }
        }}";


    }


}
