namespace Housemate.Application.Helpers;

internal static class CacheKeys
{
    public static class Student
    {
        public static string GetAll => "students-all";
        public static string Get(string id) => $"students-{id}";
        public static string GetByEmail(string email) => $"students-email-{email}";
    }   
    
    public static class HousingTask
    {
        public static string GetAll => $"housing-tasks-all";
        public static string Get(string id) => $"housing-tasks-{id}";
    }    
    
    public static class Waste
    {
        public static string GetAll => "wastes-all";
        public static string Get(string id) => $"wastes-{id}";
    }
}