using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using Newtonsoft.Json;

namespace eljur_notifier.eljur_notifier.MsDb
{
    class EljurApiRequester
    {
        internal protected Message message { get; set; }

        class Data
        {
            public Response Response;
        }

        class Response
        {
            public int Count;
            public Item[] Items;
        }

        class Item
        {
            public int Id;
            public int Owner_id;
            public string Artist;
            public string Title;
            public int Duration;
            public string Url;
            public int Genre_id;

            public override string ToString()
            {
                return string.Format("{0} ({1})", Title, Url);
            }
        }


        string json = @"{""response"": {
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
