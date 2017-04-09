using System.Runtime.Serialization;
using Aqar.Engine.BusinessEntities;
using Aqar.Engine.Helper;
using Aqar.Engine.Linq;
//using Aqar.Engine.Notifications;
using AqarSquare.Engine;
using Aqar.Engine.BusinessEntities.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

namespace Aqar.Engine
{
  public class EngineManager : IAqarService
  {
    public HeaderData Header { get; set; }
    DataClasses1DataContext dbContext = new DataClasses1DataContext();
    public EngineManager()
    {
      Header = new HeaderData();
      if (WebOperationContext.Current != null)
      {
        IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
        WebHeaderCollection headers = request.Headers;
        foreach (string key in headers.AllKeys)
        {
          switch (key)
          {
            case "CurrentLanguage":
              if (headers[key].ToLower() == "ar".ToLower())
                Header.CurrentLanguage = Languages.Arabic;
              else
                Header.CurrentLanguage = Languages.English;
              break;
            case "CurrentDevice":
              if (headers[key].ToLower() == "iOS".ToLower())
                Header.CurrentDevice = Devices.iOS;
              else if (headers[key].ToLower() == "Android".ToLower())
                Header.CurrentDevice = Devices.Android;
              else
                Header.CurrentDevice = Devices.Other;
              break;
            case "UserToken":
              Header.UserToken = headers[key];
              break;
            case "AppVersion":
              Header.AppVersion = headers[key];
              break;
            default:
              break;
          }
        }
      }
    }

    public Stream SearchOption(RequestClass requestClass)
    {
      var returnSearchOption = new List<SearchOptions>();
      var result = new Result();
      var contractObj = ContractTypesList(requestClass);
      var propertyTypeObj = PropertyTypesList(requestClass);
      var priceRangeObj = PriceRangeList();
      var spaceRangeObj = SpaceRangeList();
      var cityObj = CityList(requestClass);
      if (contractObj.Any())
      {
        returnSearchOption.Add(new SearchOptions
        {
          ContractType = contractObj,
          Property = propertyTypeObj,
          PriceRange = priceRangeObj,
          SpaceRange = spaceRangeObj,
          City = cityObj
        });


        result.Data = returnSearchOption;

      }
      else
        result.Error = Constants.Empty;
      return Result.ToStream(result);

    }


    public Stream Square(RequestClass requestClass)
    {
      var returnContractType = new List<SquareService>();
      var result = new Result();
      var convertCityId = Convert.ToInt32(requestClass.CityId);
      var cityObj = dbContext.Squares.Where(x => x.Status == true && x.CityId == convertCityId).ToList().OrderByDescending(x => x.Id);

      if (cityObj.Any())
      {
        foreach (var item in cityObj)
        {
          returnContractType.Add(new SquareService
          {
            Id = item.Id,
            Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title
          });
        }


        result.Data = returnContractType;

      }
      else
        result.Error = Constants.Empty;
      return Result.ToStream(result);

    }


    public Stream Search(RequestClass requestClass)
    {
      var returnProperty = new List<PropertyService>();
      var result = new Result();
      var images = new Images();
      var convertPriceFrom = 0;
      var convertPriceTo = 0;
      var convertSpaceFrom = 0;
      var convertSpaceTo = 0;
      if (requestClass.PriceRangeId != null && requestClass.PriceRangeId != 0)
      {
        var priceRange = HelperMethods.GetPriceRangeId(requestClass.PriceRangeId);
        convertPriceFrom = Convert.ToInt32(priceRange.From);
        convertPriceTo = Convert.ToInt32(priceRange.To);
      }
      if (requestClass.SpaceRangeId != null && requestClass.SpaceRangeId != 0)
      {
        var spaceRange = HelperMethods.GetSpaceRangeId(requestClass.SpaceRangeId);
        convertSpaceFrom = Convert.ToInt32(spaceRange.From);
        convertSpaceTo = Convert.ToInt32(spaceRange.To);
      }

      var propertyObj = dbContext.Properties.Where(x =>
        // requestClass.ContractTypeId == null ? x.ContractType == null : x.ContractType == requestClass.ContractTypeId
        //IsPublished || requestClass.SquareId == null ? x.Area == null : x.Area == requestClass.SquareId
        //|| requestClass.PriceRangeId == null ? x.Price == null : x.Price >= convertPriceFrom && x.Price <= convertPriceTo

     (
     requestClass.ContractTypeId == null ? x.ContractType == null : x.ContractType == requestClass.ContractTypeId && (x.Approved == true && x.IsPublished == true)
      || requestClass.PriceRangeId == null ? x.Price == null : x.Price >= convertPriceFrom && x.Price <= convertPriceTo && (x.Approved == true && x.IsPublished == true)
      || requestClass.SquareId == null ? x.Area == null : x.Area == requestClass.SquareId && (x.Approved == true && x.IsPublished == true)
      || requestClass.SpaceRangeId == null ? x.Space == null : x.Space >= convertSpaceFrom && x.Space <= convertSpaceTo && (x.Approved == true && x.IsPublished == true)
      && (x.Approved == true && x.IsPublished == true)
      ) && (x.Approved == true && x.IsPublished == true)

        ).ToList();
      if (propertyObj.Any())
      {
        foreach (var item in propertyObj)
        {
          var propertyType = HelperMethods.GetPropertyTypeById(requestClass, Convert.ToInt32(item.PropertyType));
          if (propertyType == null)
          {
            result.Error = "Property_Not_Exist";
            return Result.ToStream(result);
          }
          var contractType = HelperMethods.GetContractTypeById(requestClass, Convert.ToInt32(item.ContractType));
          if (contractType == null)
          {
            result.Error = "Contract_Not_Exist";
            return Result.ToStream(result);
          }
          var propertyImages = HelperMethods.GetAllPropertyImages(Convert.ToInt32(item.Id));
          if (propertyImages == null)
          {
            result.Error = "No_Images";
            return Result.ToStream(result);
          }
          var propertyImages360 = HelperMethods.GetAllPropertyImages360(Convert.ToInt32(item.Id));
          if (propertyImages360 == null)
          {
            result.Error = "No_360_Images";
            return Result.ToStream(result);
          }
          returnProperty.Add(new PropertyService
          {
            Id = item.Id,
            Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title,
            Description = (requestClass.Lang == "ar") ? item.DescriptionAr : item.Description,
            Address = (requestClass.Lang == "ar") ? item.AddressAr : item.Address,
            Price = item.Price,
            Currency = item.Currency,
            Space = item.Space,
            PropertyId = item.PropertyId,
            PropertyTypeName = propertyType,
            ContractTypeName = contractType,
            BedroomNo = item.BedroomNo,
            BathroomNo = item.BathroomNo,
            ReceptionNo = item.ReceptionNo,
            Floor = item.Floor,
            Lift = item.Lift,
            Balacony = item.Balacony,
            Garden = item.Garden,
            Garage = item.Garage,
            Pool = item.Pool,
            Late = item.Late,
            Long = item.Long,
            CreatedDate = item.CreatedDate,
            ImagesList = propertyImages,
            ImagesList360 = propertyImages360,
            UserInCharge = HelperMethods.InchargePropetryUser(item.UserInCharge)
          });
        }


        result.Data = returnProperty;

      }
      return Result.ToStream(result);

    }

    #region Hash serivces
    public Stream ContractTypeList(RequestClass requestClass)
    {
      var returnContractType = new List<ContractTypeService>();
      var result = new Result();

      var typeObj = dbContext.ContractTypes.Where(x => x.Status == true).ToList();
      if (typeObj.Any())
      {
        foreach (var item in typeObj)
        {
          returnContractType.Add(new ContractTypeService
          {
            Id = item.Id,
            Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title

          });
        }


        result.Data = returnContractType;

      }
      else
        result.Error = Constants.Empty;
      return Result.ToStream(result);

    }

    public Stream PropertyTypeList(RequestClass requestClass)
    {
      var returnContractType = new List<PropertyTypeService>();
      var result = new Result();

      var typeObj = dbContext.PropertyTypes.Where(x => x.Status == true).ToList();
      if (typeObj.Any())
      {
        foreach (var item in typeObj)
        {
          returnContractType.Add(new PropertyTypeService
          {
            Id = item.Id,
            Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title

          });
        }


        result.Data = returnContractType;

      }
      else
        result.Error = Constants.Empty;
      return Result.ToStream(result);

    }

    public Stream PriceRange(RequestClass requestClass)
    {
      var returnPriceRange = new List<PriceRangeService>();
      var result = new Result();

      var priceObj = dbContext.PriceAverages.ToList().OrderByDescending(x => x.Id);

      if (priceObj.Any())
      {
        foreach (var item in priceObj)
        {
          returnPriceRange.Add(new PriceRangeService
          {
            Id = item.Id,
            Range = item.PriceFrom + " : " + item.PriceTo
          });
        }


        result.Data = returnPriceRange;

      }
      else
        result.Error = Constants.Empty;
      return Result.ToStream(result);

    }

    public Stream SpaceRange(RequestClass requestClass)
    {
      var returnSpaceRange = new List<SpaceRangeService>();
      var result = new Result();

      var spaceObj = dbContext.SpaceAverages.ToList().OrderByDescending(x => x.Id);

      if (spaceObj.Any())
      {
        foreach (var item in spaceObj)
        {
          returnSpaceRange.Add(new SpaceRangeService
          {
            Id = item.Id,
            Range = item.SpaceFrom + " : " + item.SpaceTo
          });
        }


        result.Data = returnSpaceRange;

      }
      else
        result.Error = Constants.Empty;
      return Result.ToStream(result);

    }
    public Stream City(RequestClass requestClass)
    {
      var returnContractType = new List<CityService>();
      var result = new Result();

      var cityObj = dbContext.Cities.Where(x => x.Status == true).ToList().OrderByDescending(x => x.Id);

      if (cityObj.Any())
      {
        foreach (var item in cityObj)
        {
          returnContractType.Add(new CityService
          {
            Id = item.Id,
            Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title
          });
        }


        result.Data = returnContractType;

      }
      else
        result.Error = Constants.Empty;
      return Result.ToStream(result);

    }

    #endregion
  }


  public class RequestClass
  {
    [DataMember]
    public string CityId { get; set; }
    [DataMember]
    public string Lang { get; set; }
    [DataMember]
    public int SquareId { get; set; }
    [DataMember]
    public int ContractTypeId { get; set; }
    [DataMember]
    public int PropertyTypeId { get; set; }
    [DataMember]
    public int PriceRangeId { get; set; }
    [DataMember]
    public int SpaceRangeId { get; set; }
  }

}
