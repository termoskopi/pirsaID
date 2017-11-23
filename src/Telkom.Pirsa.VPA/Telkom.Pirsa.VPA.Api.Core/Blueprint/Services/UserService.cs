using Newtonsoft.Json.Linq;
using Research.Web.Nancy.Application.Data.BusinessLogic;
using Research.Web.Nancy.Application.Data.Core;
using System;
using Research.Web.Nancy.Application.Data.BusinessModel;
using System.Collections.Generic;
using System.Linq;

namespace Research.Web.Nancy.Application.Core.Blueprint.Services
{
  public class UserService
  {
    private readonly DataAccessManager _manager;
    private readonly IRepository _userRepository;
    private readonly IRepository _tokenRepository;

    public UserService()
    {
      throw new Exception("This default constructor has been disbaled. All Service instance only generated on ApplicationServiceBuilder");
    }

    public UserService(DataAccessManager manager)
    { 
      _manager = manager;
      _userRepository = new UserRepository(_manager.ConnectionManager);
      _tokenRepository = new AccessTokenRepository(manager.ConnectionManager);
    }

    public bool CreateUser(JObject user)
    {
      try
      {
        if (user["Username"] == null || user["Password"] == null)
          throw new Exception("Request body is required! Please add Username and Password element!");

        IDataModel model = new User()
        {
          Username = user["Username"].ToString(),
          Password = TokenProvider.HashPassword(user["Password"].ToString())
        };

        return _userRepository.Create(model);
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }


    public JArray GetAllUsers()
    {
        try
        {
            var result = new JArray();
            var data = _userRepository.Get();
            foreach (IDataModel model in data)
            {
                result.Add(JObject.Parse(model.Json));
            }

            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public JObject FindUser(object id)
    {
      try
      {
        var data = _userRepository.Get(id);
        if (data == null)
          return null;
        return JObject.Parse(data.Json);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public JObject Login(JObject user)
    {
      try
      {
        if (user["Username"] == null || user["Password"] == null)
          throw new Exception("Request body is required! Please add Username and Password element!");

        IDataModel model = new User()
        {
          Username = user["Username"].ToString(),
          Password = TokenProvider.HashPassword(user["Password"].ToString())
        };
        IList<Metadata> filter = new List<Metadata>();
        filter.Add(model.ColumnsMetadata.Where(x => x.Database == User.ColumnUsername).First());
        filter.Add(model.ColumnsMetadata.Where(x => x.Database == User.ColumnPassword).First());
        var data = _userRepository.Get(model, filter);
        if (data == null || data.Count == 0)
          return null;
        var result = JObject.Parse(data.First().Json);
        if(result == null)
          throw new Exception("Specified user not found!");

        var token = new AccessToken();
        filter.Clear();
        filter.Add(token.ColumnsMetadata.Where(x => x.Database == AccessToken.ColumnUserId).First());
        filter.Add(token.ColumnsMetadata.Where(x => x.Database == AccessToken.ColumnIsActive).First());
        token.UserId = Convert.ToInt32(result[User.ColumnId]);
        token.IsActive = 1;
        var tokens = _tokenRepository.Get(token, filter);
        var tokenArray = new JArray();
        foreach (var tokenItem in tokens)
        {
          tokenArray.Add(JObject.Parse(tokenItem.Json));
        }
        var activeToken = string.Empty;
        if (tokens == null || tokens.Count == 0)
        {
          token = new AccessToken();
          token.Token = TokenProvider.GenerateToken(result[User.ColumnUsername].ToString());
          token.UserId = Convert.ToInt32(result[User.ColumnId]);
          token.IsActive = 1;
          token.CreatedDate = DateTime.Now;
          var tokenResult = _tokenRepository.Create(token);
          if (!tokenResult)
            throw new Exception("Token creation failed!");
          activeToken = token.Token;
        }
        else
          activeToken = tokens.First().GetProperty(AccessToken.ColumnToken).ToString();


        result.Add(new JProperty("Tokens", tokenArray));
        result.Add(new JProperty("ActiveToken", activeToken));
        //return result; == To be discussed later
        return new JObject(new JProperty("Token", activeToken));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool Update(JObject user, object id)
    {
      try
      {
        IDataModel model = new User();
        foreach(var token in user)
        {
          if(token.Key.Trim().ToUpper() == "PASSWORD")
            model.SetProperty(token.Key, TokenProvider.HashPassword(token.Value.ToString()));
          else if(token.Key.Trim().ToUpper() == "USERNAME")
            model.SetProperty(token.Key, token.Value.ToString());
          else 
            model.SetProperty(token.Key, token.Value);
        }
        IList<Metadata> filter = new List<Metadata>();
        filter.Add(model.ColumnsMetadata.Where(x => x.IsPrimary).First());
        var success = _userRepository.Update(model, filter);
        
        return success;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

  }
}
