
using System;

namespace Aqar.Engine.BusinessEntities.Service
{

  public class SystemUserService
  {
     public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string PhoneNo1 { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Online { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UserType { get; set; }
        public string UniqueId { get; set; }
        public string UserImage { get; set; }
  }
}