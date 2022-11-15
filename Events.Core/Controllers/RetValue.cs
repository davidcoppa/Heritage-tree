using Events.Core.DTOs;

namespace Events.Core.Controllers
{
    internal class PersonVal
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int Id { get; set; }



        public string Class { get; set; }
        public string? TextClass { get; set; }
        public List<ExtraData> Extra { get; set; }

        public List<Marriages>? Marriages { get; set; }
        public bool IsMarriage { get; set; }
    }
    internal class Marriages
    {
        public PersonVal spouse { get; set; }
        public List<PersonVal> Children { get; set; }

    }
    internal class ExtraData
    {
        public string NickName { get; set; }

    }
}


//[{
//    "name": "Niclas Superlongsurname",
//    "class": "man",
//    "textClass": "emphasis",
//    "marriages": [{
//      "spouse": {
//        "name": "Iliana",
//        "class": "woman",
//        "extra": {
//          "nickname": "Illi"
//        }
//      },
//      "children": [{
//        "name": "James",
//        "class": "man",
//        "marriages": [{
//          "spouse": {
//            "name": "Alexandra",
//            "class": "woman"
//          },
//          "children": [{
//            "name": "Eric",
//            "class": "man",
//            "marriages": [{
//              "spouse": {
//                "name": "Eva",
//                "class": "woman"
//              }
//            }]
//          }, {
//    "name": "Jane",
//            "class": "woman"
//          }, {
//    "name": "Jasper",
//            "class": "man"
//          }, {
//    "name": "Emma",
//            "class": "woman"
//          }, {
//    "name": "Julia",
//            "class": "woman"
//          }, {
//    "name": "Jessica",
//            "class": "woman"
//          }]
//        }]
//      }]
//    }]
//  }];