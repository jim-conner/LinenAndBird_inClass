using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird_inClass.Models
{
    public class Badge
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BadgeDefinitionId { get; set; }
        public DateTime DateAcheived  { get; set; }
    }
    // below is mock-up of models we'd need to complete
    // basic requirements for the acheviment feature

    public class BadgeDefinition
    {
        public Guid Id { get; set; }
        public string BadgeName { get; set; }
        public string ImgUrl { get; set; }
    }

    public class User //the "Rower" or "Member" at Hydrow
    {
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
