namespace ChatApi.Extensions
{
    public static class DateTimeExtensions
    {

        
            public static int CalculateAge(this DateTime? dateOfBirth)
            {
                if (dateOfBirth == null)
                    return 0;

                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Value.Year;

                if (dateOfBirth.Value.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }
    }

