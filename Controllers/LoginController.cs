//using System.Web.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using WeActiveLibrary.Models;
using WeActiveWebservice.Database;
using Microsoft.Extensions.Options;
using WeActiveWebservice.Models.Local;
using System.Diagnostics;
using WeActiveLibrary.Util;

namespace WeActiveWebservice.Controllers
{

    [Route("api/[controller]")]
    public class LoginController : Controller {
        
        // Inject below part to any controller that needs to work with database
        private readonly ConnectionStrings _connectionStrings;


        public LoginController(IOptions<ConnectionStrings> options) {
            
            this._connectionStrings = options.Value;
            
        }
        // Inject above with any controller that needs to work with database


        // GET api/values
        [HttpGet("Index")]
        public string Get() {
            
            return "You are in the Login controller inside the Get method";
        }

        [HttpGet("test")]
        public string test() {

            DataManager dm = new DataManager(_connectionStrings.DefaultConnection);

            User user = dm.GetUser("test@test.gmail.com");

            return user.Email.ToString();

        }

        [HttpPost("Register")]
        public string Register([FromBody]User user) {
            
                    if(user == null) {
                
                return "Bad Request";
            }

            DataManager dm = new DataManager(_connectionStrings.DefaultConnection);

            dm.RegisterUser(user);

            return "User is regsitered";

        
        }

       
        // /api/Login/login
        // We populate the session model with the request data from the Request body
        [HttpPost("login")]
        public string Login([FromBody]Session session) {

            try {

                if (session == null) {
                    
                   // Console.WriteLine("Bad request");
                    return "Bad Request";
                }

                DataManager dataManager = new DataManager(_connectionStrings.DefaultConnection);

                // Get User From Db using data from session
                User user = dataManager.GetUser(session.User.Email);

                if (user == null) {

                    return String.Format("Could not find user account for {0},", session.User.Email);

                }

                if (user.Activated == false) {

                    return String.Format("Your User account {0} has not be activated", session.User.Email);
                }


                // Basic User authentication has been complete

                // Get the gym that the User belongs to.

                Gym gym = dataManager.GetGym(user.Id);


                if (gym == null) {
                    // User does not belong to a gym.

                    return String.Format("{0} you must registered with a Gym", user.Email);
                    
                }

                // Need to add a check to determine whether
                // the User has a license

                Session activeSession = dataManager.GetActiveSession(user.Id);

                Session newSession = new Session();

                // If User already has an existing session
                if (activeSession != null) {

                    return activeSession;

                } else {

                    // create a session
                    newSession.User = user;
                    newSession.SessionKey = StringUtil.GenerateGuid();

                    // Add Session to Database
                    dataManager.AddSession(newSession);

                }


               // dataManager.

               //  dataManager.GetUser();

                //  return test.GetConnection();
                return "";

                /*
                User user = dataManager.GetUser(session.User.Email);

                if (user == null)
                    Console.WriteLine("Not user found");

                if (user.Activated != true) {
                    Console.WriteLine("User is not activated!");
                }*/


            } catch (Exception e) {

                return "Exception thrown ";
            }

        }


    }
}
