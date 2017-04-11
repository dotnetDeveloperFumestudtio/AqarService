using System.IO;
using Aqar.Engine;
using System.ServiceModel.Web;
using Aqar.Engine.Common;
using RequestClass = Aqar.Engine.RequestClass;

namespace Aqar.Service
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
  // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
  public class AqarService : IAqarService
  {
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Register")]
    public Stream QuickRegister(UserRequestClass userrequestClass)
    {
      return new EngineManager().QuickRegister(userrequestClass);
    }
      
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchOptionList")]
    public Stream SearchOption(RequestClass requestClass)
    {
      return new EngineManager().SearchOption(requestClass);
    }
     
   
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SquareList")]
    public Stream Square(RequestClass requestClass)
    {
      return new EngineManager().Square(requestClass);
    }

 
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Search")]
    public Stream Search(RequestClass requestClass)
    {
      return new EngineManager().Search(requestClass);
    }


    #region Hash serivces

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ContractList")]
    public Stream ContractTypeList(RequestClass requestClass)
    {
      return new EngineManager().ContractTypeList(requestClass);
    }

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PropertyTypeList")]
    public Stream PropertyTypeList(RequestClass requestClass)
    {
      return new EngineManager().PropertyTypeList(requestClass);
    }
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PriceRangeList")]
    public Stream PriceRange(RequestClass requestClass)
    {
      return new EngineManager().PriceRange(requestClass);
    }

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SpaceRangeList")]
    public Stream SpaceRange(RequestClass requestClass)
    {
      return new EngineManager().SpaceRange(requestClass);
    }


    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CityList")]
    public Stream City(RequestClass requestClass)
    {
      return new EngineManager().City(requestClass);
    }

    #endregion
  }
}
