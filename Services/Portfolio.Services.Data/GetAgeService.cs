namespace Portfolio.Services.Data
{
    using System;

    public class GetAgeService : IGetAgeService
    {
        public string GetAge(string date)
        {
            var birthday = DateTime.Parse(date);
            var result = DateTime.UtcNow.Date.Year - DateTime.Parse(date).Year;

            if (birthday.Date.Month > DateTime.UtcNow.Month)
            {
                result -= 1;
            }
            else if (birthday.Date.Month == DateTime.UtcNow.Month)
            {
                if (birthday.Day >= DateTime.UtcNow.Day)
                {
                    result -= 1;
                }
            }

            return result.ToString();
        }
    }
}
