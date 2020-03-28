using challenge.DAL;
using challenge.DAL.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Utils.Helpers
{
    internal class DataHelper
    {
        public static void Initialize(ChallengeContext context, ILogger logger = null)
        {
            context.Database.EnsureCreated();
            logger.LogInformation("Created Database");

            if (context.PaymentTypes.Any())
            {
                logger.LogInformation("The Database alredy exists.");
                return;
            }

            logger.LogInformation("Load data paymentTypes");
            var paymentTypes = GetPaymentType().ToArray();
            context.AddRange(paymentTypes);
            context.SaveChanges();

            logger.LogInformation("Load data users");
            var users = GetUserFromFile().ToArray();
            var address = GetAddressFromFile().ToArray();

            for(int index = 0; index < users.Count(); index++)
            {
                users[index].Direction = address[index];
            }

            context.AddRange(users);
            context.SaveChanges();

            logger.LogInformation("Load data payments");
            var payments = GetPaymentFromFile();

            foreach(var payment in payments)
            {
                int index = new Random().Next(paymentTypes.Count() -1 );
                payment.PaymentType = paymentTypes[index];
            }

            context.AddRange(payments);
            context.SaveChanges();

            logger.LogInformation("finisth to initialize database");
        }

        private static IEnumerable<PaymentType> GetPaymentType()
        {
            var dataJson = GetDataFromFile("PaymentType.json");
            IEnumerable<PaymentType> data = JsonConvert.DeserializeObject<List<PaymentType>>(dataJson);
            return data;
        }

        public static IEnumerable<User> GetUserFromFile()
        {
            var dataJson = GetDataFromFile("Users.json");
            IEnumerable<User> data = JsonConvert.DeserializeObject<List<User>>(dataJson);
            return data;            
        }

        public static IEnumerable<Payment> GetPaymentFromFile()
        {
            var dataJson = GetDataFromFile("Payment.json");
            IEnumerable<Payment> data = JsonConvert.DeserializeObject<List<Payment>>(dataJson);
            return data;
        }

        public static IEnumerable<Address> GetAddressFromFile()
        {
            var dataJson = GetDataFromFile("Address.json");
            IEnumerable<Address> data = JsonConvert.DeserializeObject<List<Address>>(dataJson);
            return data;
        }

        private static string GetDataFromFile(string fileName)
        {            
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mocks", fileName);
            return File.ReadAllText(path);
        }
    }
}
