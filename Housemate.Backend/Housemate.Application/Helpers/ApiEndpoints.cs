namespace Housemate.Application.Helpers;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Auth
    {
        private const string Base = $"{ApiBase}/auth";

        public const string Register = $"{Base}/register";
        public const string Login = $"{Base}/login";
    }
    
    public static class Students
    {
        private const string Base = $"{ApiBase}/students";
        
        public const string GetAll = $"{Base}";
        public const string GetByEmail = $"{Base}/{{email}}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class HousingTasks
    {
        private const string Base = $"{ApiBase}/housingtasks";
        
        public const string GetAll = $"{Base}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    public static class Wastes
    {
        private const string Base = $"{ApiBase}/wastes";
        
        public const string GetAll = $"{Base}";
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Weather
    {
        private const string Base = $"{ApiBase}/weather";
        
        public const string GetCurrent = $"{Base}/current/{{city}}";
        public const string GetForecast = $"{Base}/forecast/{{city}}";
    }
}