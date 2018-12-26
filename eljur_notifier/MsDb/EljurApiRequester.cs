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
        internal protected JObject data { get; set; }

        public EljurApiRequester()
        {
            this.message = new Message();
        }

        public JObject getData()
        {
            dynamic data = JsonConvert.DeserializeObject(jsonStr);

            //var data = JObject.Parse(jsonStr);

            var response = data.response;
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

            //message.Display(data.ToString(), "Trace");

            message.Display(data.GetType().ToString(), "Trace");

            foreach (var item in result)
            {
                message.Display(item.ToString(), "Trace");
            }

            return data; 
        }

        String jsonStr = @"{
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
