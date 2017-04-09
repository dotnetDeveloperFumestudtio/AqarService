
using System;
using System.Collections.Generic;

namespace Aqar.Engine.BusinessEntities.Service
{

  public class SearchOptions
  {
    public List<ContractTypeService> ContractType { get; set; }
    public List<PropertyTypeService> Property { get; set; }
    public List<SpaceRangeService> SpaceRange { get; set; }
    public List<PriceRangeService> PriceRange { get; set; }
    public List<CityService> City { get; set; }
  }
}