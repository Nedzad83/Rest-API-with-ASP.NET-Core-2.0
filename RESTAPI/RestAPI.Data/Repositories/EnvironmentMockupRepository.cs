using RestAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestAPI.Data.Repositories
{
    public class EnvironmentMockupRepository
    {
        private readonly EnvironmentContext _context;

        public EnvironmentMockupRepository(EnvironmentContext context)
        {
            _context = context;
            if (context.Environments.Count() == 0)
            {
                EnvDeployment env = new EnvDeployment {
                    Ticket = "69311",
                    Type = "44423",
                    Env = "Test_Environment 3",
                    Start_time = "6/30/2016 9:13:32 AM",
                    User_id = "nedzadtest@lsi-lps.com",
                    Shortdes = "Test - RFC 332260 - WI 10377"
                };
                EnvDeployment env2 = new EnvDeployment
                {
                    Ticket = "69309",
                    Type = "44419",
                    Env = "Test_Environment 2",
                    Start_time = "6/30/2017 9:13:32 AM",
                    User_id = "nedzad.test@lsi-lps.com",
                    Shortdes = "Test_Environment Update launcher"
                };
                EnvDeployment curEnv1 = new EnvDeployment
                {
                    Ticket = "69300",
                    Id = "44423",
                    Env = "Test_Environment",
                    Start_time = "6/30/2016 9:13:32 AM",
                    User_id = "nedzadtest@lsi-lps.com",
                    Shortdes = "Test_Environment (CS 154377)"
                };
                EnvDeployment curEnv2 = new EnvDeployment
                {
                    Ticket = "69309",
                    Id = "44419",
                    Env = "Test_Environment2",
                    Start_time = "6/30/2017 9:13:32 AM",
                    User_id = "nedzad.test@lsi-lps.com",
                    Shortdes = "Test_Environment RFC 233245 WI 45674 Update launcher reference for monitoring"

                };
                _context.Environments.AddRange(env);
                _context.Environments.AddRange(env2);
                _context.CurrentEnvironments.AddRange(curEnv1);
                _context.CurrentEnvironments.AddRange(curEnv2);
                _context.SaveChanges();
            }
        }
        public IEnumerable<EnvDeployment> Get()
        {
            return _context.Environments;
        }

        public IEnumerable<EnvDeployment> GetCurrentEnvironment()
        {
            return _context.CurrentEnvironments;
        }
    }
}
