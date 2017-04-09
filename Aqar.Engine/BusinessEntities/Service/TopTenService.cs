
using System;

namespace Aqar.Engine.BusinessEntities.Service
{

  public class TopTenService
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string ContractType { get; set; }
    public string Address { get; set; }
    public int Space { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public string Currency { get; set; }
    public int BedroomNo { get; set; }
    public int ReceptionNo { get; set; }
    public int RoomsNo { get; set; }
    public int Floor { get; set; }
    public DateTime? Creatate { get; set; }
    public int? CreatedBy { get; set; }
    public string CreatedByUserName { get; set; }
    public int ViewCount { get; set; }

  }
}