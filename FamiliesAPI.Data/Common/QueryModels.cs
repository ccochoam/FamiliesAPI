using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Common
{
    public class QueryModels
    {
        public static object GetUserQuery(string parameter, string action)
        {
            return new
            {
                Action = action,
                UserId = action == "byId" ? parameter : "0",
                Username = action == "byUsername" ? parameter : string.Empty
            };
        }

        public static object UserModelDB(UserModel userModel)
        {
            return new
            {
                UserId = userModel.UserId,
                Username = userModel.Username,
                Password = userModel.Password,
                HashKey = userModel.HashKey,
                Name = userModel.Name,
                LastName = userModel.LastName,
                NumberId = userModel.NumberId,
                GenderId = userModel.GenderId,
                Relationship = userModel.Relationship,
                Age = userModel.Age,
                UnderAge = userModel.UnderAge,
                Birthdate = userModel.Birthdate,
                FamilyGroupId = userModel.FamilyGroupId
            };
        }

        public static object LogModelDB(LoggerModel logModel)
        {
            return new
            {
                Action = logModel.Action,
                Username = logModel.Username,
                Process = logModel.Process,
                Response = logModel.Response,
                Request = logModel.Request,
                Successful = logModel.Successful,
                Exception = logModel.Exception
            };
        }
    }
}
