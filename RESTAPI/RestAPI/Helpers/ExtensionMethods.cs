using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestAPI.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Method used to validad if the change was success
        /// </summary>
        /// <param name="RowsAffected"></param>
        /// <returns></returns>
        public static IActionResult GetStatusResult(this int RowsAffected)
        {
            return new StatusCodeResult(RowsAffected > 0 ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError);
        }
    }
}
