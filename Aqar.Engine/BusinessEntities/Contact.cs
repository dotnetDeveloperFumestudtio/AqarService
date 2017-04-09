using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aqar.Engine.Entities;

namespace Aqar.Engine.BusinessEntities
{
 public class Contact: BusinessEntityBase
  {
    #region Constructor(s)
    public Contact()
      : base()
    {
    }

    public Contact(ContactU entity, HeaderData header)
      : base(header)
    {
      Convert<ContactU>(entity);
    }
    #endregion
     
    #region Properties
    public int ContactId { get; set; }


    [Required]
    [StringLength(300)]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    
    public string Email { get; set; }
  
    

    #endregion
  }
}
