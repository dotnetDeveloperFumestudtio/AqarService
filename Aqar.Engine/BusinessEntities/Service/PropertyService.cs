using System.Web.Script.Serialization;
//using AqarSquare.Engine.Entities;
using System;
using System.Collections.Generic;
using Aqar.Engine.Linq;

namespace Aqar.Engine.BusinessEntities.Service
{

  public class PropertyService
  {
    public int Id { get; set; }
    public string Title { get; set; }
    [ScriptIgnore]
    public int? UserId { get; set; }
    [ScriptIgnore]
    public bool? Approved { get; set; }
    public string Description { get; set; } 
    public int? Price { get; set; }
    public string Currency { get; set; }
    public string Late { get; set; }
    public string Long { get; set; }
    public int? Space { get; set; }

    [ScriptIgnore]
    public int? RoomsNo { get; set; }
    public int? BathroomNo { get; set; }
    public int? Floor { get; set; }
    public string PropertyId { get; set; } 
    public string PropertyTypeName { get; set; }
    public string ContractTypeName { get; set; }
    [ScriptIgnore]
    public int? PropertyType { get; set; }
    public int? BedroomNo { get; set; }
    [ScriptIgnore]
    public int? ReceptionNo { get; set; }
    [ScriptIgnore]
    public bool? Lift { get; set; }
    [ScriptIgnore]
    public bool? AirCondtion { get; set; }
    [ScriptIgnore]
    public bool? Balacony { get; set; }
    [ScriptIgnore]
    public bool? Garden { get; set; }
    [ScriptIgnore]
    public bool? Garage { get; set; }
    [ScriptIgnore]
    public bool? Pool { get; set; }
    public string Address { get; set; } 
    [ScriptIgnore]
    public int? Area { get; set; }
    public DateTime? CreatedDate { get; set; }
    [ScriptIgnore]
    public int? CreatedBy { get; set; }
    [ScriptIgnore]
    public int? ContractType { get; set; }
    [ScriptIgnore]
    public List<ImageBedroom> ListImageBedroom { get; set; }
    [ScriptIgnore]
    public List<ImageBalacony> ListImageBalacony { get; set; }
    [ScriptIgnore]
    public List<ImagePool> ListImagePool { get; set; }
    [ScriptIgnore]
    public List<ImageReception> ListImageReception { get; set; }
    [ScriptIgnore]
    public List<ImageGarden> ListImageGarden { get; set; }

    [ScriptIgnore]
    public List<ImageBathroom> ListImageBathroom { get; set; }

    public List<string> ImagesList { get; set; }
    public List<string> ImagesList360 { get; set; } 
    public InchargePropetryUser UserInCharge { get; set; }


  }
}