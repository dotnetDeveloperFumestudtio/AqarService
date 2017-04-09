﻿using Aqar.Engine.BusinessEntities.Service;
using Aqar.Engine.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Aqar.Engine.Helper
{

  public class HelperMethods
  {
    static DataClasses1DataContext dbContext = new DataClasses1DataContext();

    //public PropertyService GetPropertyById(string propertyId)
    //{
    //  var result = new PropertyService();

    //  var viewObj = dbContext.Properties.FirstOrDefault(item => item.PropertyId == propertyId);
    //  if (viewObj != null)
    //  {
    //    result.Id = viewObj.Id;
    //    result.PropertyId = viewObj.PropertyId;
    //    result.Title = viewObj.Title;
    //    result.TitleAr = viewObj.TitleAr;
    //    result.Description = viewObj.Description;
    //    result.DescriptionAr = viewObj.DescriptionAr;
    //    result.Address = viewObj.Address;
    //    result.AddressAr = viewObj.AddressAr;
    //    result.Late = viewObj.Late;
    //    result.Long = viewObj.Long;
    //    result.Price = viewObj.Price;
    //    result.BathroomNo = viewObj.BathroomNo;
    //    result.BedroomNo = viewObj.BedroomNo;
    //    result.RoomsNo = viewObj.RoomsNo;
    //    result.ReceptionNo = viewObj.ReceptionNo;
    //    result.Floor = viewObj.Floor;
    //    result.Balacony = viewObj.Balacony;
    //    result.Garage = viewObj.Garage;
    //    result.Garden = viewObj.Garden;
    //    result.Pool = viewObj.Pool;
    //    result.Lift = viewObj.Lift;
    //    result.Area = viewObj.Area;
    //    //  result.PropertyTypeName = GetPropertyTypeById(viewObj.PropertyType);
    //    result.PropertyType = viewObj.PropertyType;
    //    result.Currency = viewObj.Currency;
    //    result.Approved = viewObj.Approved;
    //    result.CreatedBy = viewObj.CreatedBy;
    //    result.CreatedByUserName = GetUserNameById(viewObj.CreatedBy);
    //    result.CreatedDate = viewObj.CreatedDate;
    //    result.ApprovedBy = viewObj.ApprovedBy;
    //    result.ApprovedByUserName = GetUserNameById(viewObj.ApprovedBy);
    //    result.ApprovedDate = viewObj.ApprovedDate;
    //    result.UserId = viewObj.UserId;
    //    result.ContractType = viewObj.ContractType;
    //    //result.ListImageBedroom = GetAllImageBedroom(viewObj.Id);
    //    //result.ListImageBalacony = GetAllImageBalacony(viewObj.Id);
    //    //result.ListImagePool = GetAllImagePool(viewObj.Id);
    //    //result.ListImageReception = GetAllImageReception(viewObj.Id);
    //    //result.ListImageGarden = GetAllImageGarden(viewObj.Id);
    //    //result.ListImageBathroom = GetAllImageBathroom(viewObj.Id);
    //  }

    //  return result;
    //}

    public static List<string> GetAllPropertyImages360(int propertyId)
    {
      var returnImages = new List<string>();

      var imagesList = dbContext.Image360s.Where(x => x.PropertyId == propertyId && x.Status == true).ToList();
      foreach (var image360 in imagesList)
      {

        returnImages.Add(
            image360.Image
        );

      }
      return returnImages;

      //return imagesList.Select(image360 => new Images
      //{
      //  ImageUrl = image360.Image
      //}).ToList();
    }

    public static List<string> GetAllPropertyImages(int propertyId)
    {
      var returnImages = new List<string>();
      var bedroomImages = GetAllImageBedroom(propertyId);
      var bathroomImages = GetAllImageBathroom(propertyId);
      var poolImages = GetAllImagePool(propertyId);
      var gardenImages = GetAllImageGarden(propertyId);
      var receptionImages = GetAllImageReception(propertyId);
      var balaconyImages = GetAllImageBalacony(propertyId);
      var imagesList = new List<Images>();
      imagesList = bedroomImages.Select(bedroomImage => new Images
    {
      ImageUrl = bedroomImage.Image
    }).ToList();
      imagesList.AddRange(bathroomImages.Select(bathroomObj => new Images
      {
        ImageUrl = bathroomObj.Image
      }));
      imagesList.AddRange(poolImages.Select(poolObj => new Images
      {
        ImageUrl = poolObj.Image
      }));

      imagesList.AddRange(gardenImages.Select(gardenObj => new Images
      {
        ImageUrl = gardenObj.Image
      }));

      imagesList.AddRange(receptionImages.Select(receptionObj => new Images
      {
        ImageUrl = receptionObj.Image
      }));
      imagesList.AddRange(balaconyImages.Select(balaconyObj => new Images
      {
        ImageUrl = balaconyObj.Image
      }));
      foreach (var image in imagesList)
      {
        returnImages.Add(
            image.ImageUrl
        );

      }
      return returnImages;
    }

    public static List<ImageBedroom> GetAllImageBedroom(int propertyId)
    {
      return dbContext.ImageBedrooms.Where(x => x.PropertyId == propertyId && x.Status == true).ToList();
    }

    public static List<ImagePool> GetAllImagePool(int propertyId)
    {
      return dbContext.ImagePools.Where(x => x.PropertyId == propertyId && x.Status == true).ToList();
    }

    public static List<ImageGarden> GetAllImageGarden(int propertyId)
    {
      return dbContext.ImageGardens.Where(x => x.PropertyId == propertyId && x.Status == true).ToList();
    }

    public static List<ImageReception> GetAllImageReception(int propertyId)
    {
      return dbContext.ImageReceptions.Where(x => x.PropertyId == propertyId && x.Status == true).ToList();
    }

    public static List<ImageBathroom> GetAllImageBathroom(int propertyId)
    {
      return dbContext.ImageBathrooms.Where(x => x.PropertyId == propertyId && x.Status == true).ToList();
    }

    public static List<ImageBalacony> GetAllImageBalacony(int propertyId)
    {
      return dbContext.ImageBalaconies.Where(x => x.PropertyId == propertyId && x.Status == true).ToList();
    }

    public static string GetPropertyTypeById(RequestClass requestClass, int? propertyTypeId)
    {
      var obj = dbContext.PropertyTypes.FirstOrDefault(p => p.Id == propertyTypeId);

      if (obj != null)

        return (requestClass.Lang == "ar") ? obj.TitleAr : obj.Title;
      else
        return null;
    }

    public static string GetContractTypeById(RequestClass requestClass, int? contractTypeId)
    {
      var obj = dbContext.ContractTypes.FirstOrDefault(p => p.Id == contractTypeId);

      if (obj != null)
        return (requestClass.Lang == "ar") ? obj.TitleAr : obj.Title;
      else
        return null;
    }

    public string GetUserNameById(int? id)
    {
      SystemUser _user = dbContext.SystemUsers.FirstOrDefault(user => user.Id == id);

      if (_user != null)
        return _user.FirstName + " " + _user.LastName;
      else
        return null;
    }

    public static InchargePropetryUser InchargePropetryUser(int? userId)
    {
      var result = new InchargePropetryUser();
      var obj = dbContext.UserContacts.FirstOrDefault(p => p.UserId == userId);

      //if (obj != null)
      //{
      var phone = new List<Phone>();

      //var obj = Context.UserContacts.FirstOrDefault(p => p.UserId == userId);

      if (obj != null)
      {
        phone.Add(new Phone
        {
          PhoneNumber = obj.Phone
        });
        result.WhatsApp = obj.WhatsApp;
        result.Viber = obj.Viber;
        result.PhoneList = phone;
        return result;
      }
      else
        return null;
    }

    public static PriceRangeService GetPriceRangeId(int? priceRangeId)
    {
      var result = new PriceRangeService();
      var obj = dbContext.PriceAverages.FirstOrDefault(p => p.Id == priceRangeId);

      if (obj != null)
      {
        result.Id = obj.Id;
        result.From = obj.PriceFrom;
        result.To = obj.PriceTo;
        return result;
      }
      else
        return null;
    }

    public static SpaceRangeService GetSpaceRangeId(int? spaceRangeId)
    {
      var result = new SpaceRangeService();
      var obj = dbContext.SpaceAverages.FirstOrDefault(p => p.Id == spaceRangeId);

      if (obj != null)
      {
        result.Id = obj.Id;
        result.From = obj.SpaceFrom;
        result.To = obj.SpaceTo;
        return result;
      }
      else
        return null;
    }
 
  #region Helper Search Option
      private List<ContractTypeService> ContractTypesList(RequestClass requestClass)
    {
      var returnContractTy = new List<ContractTypeService>();
      var typeObj = dbContext.ContractTypes.Where(x => x.Status == true).ToList();
      foreach (var item in typeObj)
      {
        returnContractTy.Add(new ContractTypeService
        {
          Id = item.Id,
          Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title
        });
      }
      return returnContractTy;
    }
    private List<PropertyTypeService> PropertyTypesList(RequestClass requestClass)
    {
      var returnContractTy = new List<PropertyTypeService>();
      var typeObj = dbContext.PropertyTypes.Where(x => x.Status == true).ToList();
      foreach (var item in typeObj)
      {
        returnContractTy.Add(new PropertyTypeService
        {
          Id = item.Id,
          Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title
        });
      }
      return returnContractTy;
    }
    private List<PriceRangeService> PriceRangeList()
    {
      var returnPriceRange = new List<PriceRangeService>();
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
      }
      return returnPriceRange;
    }
    private List<SpaceRangeService> SpaceRangeList()
    {
      var returnSpaceRange = new List<SpaceRangeService>();
      var priceObj = dbContext.SpaceAverages.ToList().OrderByDescending(x => x.Id);

      if (priceObj.Any())
      {
        foreach (var item in priceObj)
        {
          returnSpaceRange.Add(new SpaceRangeService
          {
            Id = item.Id,
            Range = item.SpaceFrom + " : " + item.SpaceTo
          });
        }
      }
      return returnSpaceRange;
    }
    private List<CityService> CityList(RequestClass requestClass)
    {
      var returnCity = new List<CityService>();
      var cityObj = dbContext.Cities.Where(x => x.Status == true).ToList().OrderByDescending(x => x.Id);

      if (cityObj.Any())
      {
        foreach (var item in cityObj)
        {
          returnCity.Add(new CityService
          {
            Id = item.Id,
            Title = (requestClass.Lang == "ar") ? item.TitleAr : item.Title
          });
        }
      }
      return returnCity;
    }
   
    #endregion
  }

}