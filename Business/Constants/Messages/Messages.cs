using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants.Messages
{
    public static class Messages
    {
        public static string DirectorAdded = "Director added successfuly";
        public static string DirectorUpdated = "Director updated successfuly";
        public static string DirectorDeleted = "Director deleted successfuly";
        public static string MovieAdded = "Movie added successfuly";
        public static string MovieUpdated = "Movie updated successfuly";
        public static string MovieDeleted = "Movie deleted successfuly";
        public static string UserAdded = "User added successfuly";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password is wrong";
        public static string SuccessfulLogin = "Login successfuly";
        public static string UserAlreadyExist = "User already exist";
        public static string UserCreatedSuccessfuly = "User created Successfuly";
        public static string TokenCreated = "Access token created successfuly";
        public static string GenreAdded = "Genre added successfuly";
        public static string GenreDeleted = "Genre deleted successfuly";
        public static string GenreUpdated = "Genre updated successfuly";
        public static string PermissionDenied = "Permission denied";
    }
}
