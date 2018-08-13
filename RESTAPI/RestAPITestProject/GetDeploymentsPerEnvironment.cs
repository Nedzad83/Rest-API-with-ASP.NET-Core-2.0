using RestAPI.Data;
using RestAPI.Data.Models;
using RestAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestAPITestProject
{
    public class GetDeploymentsPerEnvironment
    {
        [Fact]
        public void GetAtrributePerId()
        {
            EnvDeployment env = new EnvDeployment
            {
                Ticket = "69311",
                Type = "44423",
                Env = "Test_Environment 3",
                Start_time = "6/30/2016 9:13:32 AM",
                User_id = "nedzadtest@lsi-lps.com",
                Shortdes = "Test - RFC 332260 - WI 10377"
            };
            //Task.Run(() =>
            //{
            //    IEnumerable<EnvDeployment>  result = _mockRepository.Get();
            //    Assert.NotNull(result);
            //    Console.Write(result);
            //}).GetAwaiter().GetResult();

        }
    }
}
