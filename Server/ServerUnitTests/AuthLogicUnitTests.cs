using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.ConsoleLogger;
using Server.Global.Storage;
using Server.Logic.Auth;
using Server.Logic.Auth.Models;
using Server.Logic.Auth.Responses;
using Server.Logic.Auth.Responses.Base;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ServerUnitTests;

[TestClass]
public class AuthLogicUnitTests
{
    private AuthLogicProvider _logic;
    public void Setup()
    {
        GlobalStorage storage = new GlobalStorage();
        storage.CreateUser(new User("root","rootPassword",BasicRoles.AdminRole));
        storage.CreateUser(new User("user","userPassword",BasicRoles.SimpleUserRole));
        _logic = new AuthLogicProvider(storage,new ConsoleLogger());
    }

    [TestMethod]
    public void Test_Authenticate()
    {
        Setup();
        Dictionary<Tuple<string,string>,AuthLogicAuthenticateResponse> testTable = new Dictionary<Tuple<string, string>, AuthLogicAuthenticateResponse>
        {
            [new Tuple<string, string>("user", "userPassword")] = new AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode.Ok, "user", BasicRoles.SimpleUserRole.Name),
            [new Tuple<string, string>("root", "rootPassword")] = new AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode.Ok, "root", BasicRoles.AdminRole.Name),
            [new Tuple<string, string>("notExistedUser", "notExistedPassword")] = new AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode.WrongLoginOrPassword, "", ""),
            [new Tuple<string, string>("user", "wrongPassword")] = new AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode.WrongLoginOrPassword, "", "")
        };

        foreach (var ell in testTable)
        {
            var resp = _logic.Authenticate(ell.Key.Item1,ell.Key.Item2);
            Assert.AreEqual(ell.Value.StatusCode,resp.StatusCode);
            Assert.AreEqual(ell.Value.Login,resp.Login);
            Assert.AreEqual(ell.Value.RoleName,resp.RoleName);
            // Assert.AreEqual(ell.Value.StatusCode,resp.StatusCode);
            // Assert.AreEqual(ell.Value.Login,resp.Login);
            // Assert.AreEqual(ell.Value.RoleName,resp.RoleName);
        }
    }
    [TestMethod]
    public void Test_RegisterAdminUser()
    {
        Setup();
        Dictionary<Tuple<string,string>,AuthLogicRegisterAdminUserResponse> testTable = new Dictionary<Tuple<string, string>, AuthLogicRegisterAdminUserResponse>
        {
            [new Tuple<string, string>("user1", "userPassword")] = new AuthLogicRegisterAdminUserResponse(AuthLogicResponsesStatusCode.Ok),
            [new Tuple<string, string>("root", "rootPassword")] = new AuthLogicRegisterAdminUserResponse(AuthLogicResponsesStatusCode.UserAlreadyExists),
            [new Tuple<string, string>("user", "notExistedPassword")] = new AuthLogicRegisterAdminUserResponse(AuthLogicResponsesStatusCode.UserAlreadyExists),
        };
        foreach (var ell in testTable)
        {
            var resp = _logic.RegisterAdminUser(new User(ell.Key.Item1, ell.Key.Item2));
            Assert.AreEqual(ell.Value.StatusCode,resp.StatusCode);
        }
    }

    [TestMethod]
    public void Test_RegisterSimpleUser()
    {
        Setup();
        Dictionary<Tuple<string,string>,AuthLogicRegisterSimpleUserResponse> testTable = new Dictionary<Tuple<string, string>, AuthLogicRegisterSimpleUserResponse>
        {
            [new Tuple<string, string>("user1", "userPassword")] = new AuthLogicRegisterSimpleUserResponse(AuthLogicResponsesStatusCode.Ok),
            [new Tuple<string, string>("root", "rootPassword")] = new AuthLogicRegisterSimpleUserResponse(AuthLogicResponsesStatusCode.UserAlreadyExists),
            [new Tuple<string, string>("user", "notExistedPassword")] = new AuthLogicRegisterSimpleUserResponse(AuthLogicResponsesStatusCode.UserAlreadyExists),
        };
        foreach (var ell in testTable)
        {
            var resp = _logic.RegisterSimpleUser(new User(ell.Key.Item1, ell.Key.Item2));
            Assert.AreEqual(ell.Value.StatusCode,resp.StatusCode);
        }
    }
}