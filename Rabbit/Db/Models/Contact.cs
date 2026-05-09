using System.ComponentModel.DataAnnotations;

namespace Rabbit.Db.Models
{
    public class Contact : BaseModel
    {

        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string JobTitle { get; set; }

        public string BirthDate { get; set; }

    }
}
