using System.IO;
using Aqar.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Aqar.Engine.Common;

namespace Aqar.Engine
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
  [ServiceContract]
  public interface IAqarService
  {


    [OperationContract]
    Stream QuickRegister(UserRequestClass userrequestClass);

    [OperationContract]
    Stream SearchOption(RequestClass requestClass);


    [OperationContract]
    Stream Square(RequestClass requestClass);

    [OperationContract]
    Stream Search(RequestClass requestClass);

    #region Hash serivces

    [OperationContract]
    Stream ContractTypeList(RequestClass requestClass);

    [OperationContract]
    Stream PropertyTypeList(RequestClass requestClass);

    [OperationContract]
    Stream PriceRange(RequestClass requestClass);

    [OperationContract]
    Stream SpaceRange(RequestClass requestClass);
    [OperationContract]

    Stream City(RequestClass requestClass);
    #endregion
  }
}
