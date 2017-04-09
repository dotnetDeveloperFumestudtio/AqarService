using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace AqarSquare.Engine
{

  public class Result
  {
    public string Error { get; set; }
    public Object Data { get; set; }
    public static Stream ToStream<T>(T t)
    {
      if (WebOperationContext.Current != null) WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

      var serializer = new JavaScriptSerializer();
      var output = serializer.Serialize(t);

      return new MemoryStream(Encoding.UTF8.GetBytes(output));
    }
  }
}
