﻿using AzureNamingTool.Models;
using AzureNamingTool.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AzureNamingTool.Services;
using AzureNamingTool.Attributes;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureNamingTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class AdminController : ControllerBase
    {
        private ServiceResponse serviceResponse = new();
        private Config config = GeneralHelper.GetConfigurationData();

        // POST api/<AdminController>
        /// <summary>
        /// This function will update the Admin Password. 
        /// </summary>
        /// <param name="password">string - New Admin Password</param>
        /// <param name="adminpassword">Current Admin Password</param>
        /// <returns>string - Successful update</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePassword([BindRequired][FromHeader(Name = "AdminPassword")] string adminpassword, [FromBody] string password)
        {
            try
            {
                if (adminpassword != "")
                {
                    if (adminpassword == GeneralHelper.DecryptString(config.AdminPassword, config.SALTKey))
                    {
                        serviceResponse = await AdminService.UpdatePassword(password);
                        if (serviceResponse.Success)
                        {
                            return Ok("SUCCESS");
                        }
                        else
                        {
                            return Ok("FAILURE - There was a problem updating the password.");
                        }
                    }
                    else
                    {
                        return Ok("FAILURE - Incorrect Admin Password.");
                    }

                }
                else
                {
                    return Ok("FAILURE - You must provide teh Admin Password.");
                }
            }
            catch (Exception ex)
            {
                AdminLogService.PostItem(new AdminLogMessage() { Title = "ERROR", Message = ex.Message });
                return BadRequest(ex);
            }
        }

        // POST api/<AdminController>
        /// <summary>
        /// This function will update the API Key. 
        /// </summary>
        /// <param name="apikey">string - New API Key</param>
        /// <param name="adminpassword">Current Admin Password</param>
        /// <returns>dttring - Successful update</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAPIKey([BindRequired][FromHeader(Name = "AdminPassword")] string adminpassword, [FromBody] string apikey)
        {
            try
            {
                if (adminpassword != "")
                {
                    if (adminpassword == GeneralHelper.DecryptString(config.AdminPassword, config.SALTKey))
                    {
                        serviceResponse = await AdminService.UpdateAPIKey(apikey);
                        if (serviceResponse.Success)
                        {
                            return Ok("SUCCESS");
                        }
                        else
                        {
                            return Ok("FAILURE - There was a problem updating the API Key.");
                        }
                    }
                    else
                    {
                        return Ok("FAILURE - Incorrect Admin Password.");
                    }

                }
                else
                {
                    return Ok("FAILURE - You must provide teh Admin Password.");
                }
            }
            catch (Exception ex)
            {
                AdminLogService.PostItem(new AdminLogMessage() { Title = "ERROR", Message = ex.Message });
                return BadRequest(ex);
            }
        }

        // POST api/<AdminController>
        /// <summary>
        /// This function will generate a new API Key. 
        /// </summary>
        /// <param name="adminpassword">Current Admin Password</param>
        /// <returns>string - Successful update / Generated API Key</returns>

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GenerateAPIKey([BindRequired][FromHeader(Name = "AdminPassword")] string adminpassword)
        {
            try
            {
                if (adminpassword != "")
                {
                    if (adminpassword == GeneralHelper.DecryptString(config.AdminPassword, config.SALTKey))
                    {
                        serviceResponse = await AdminService.GenerateAPIKey();
                        if (serviceResponse.Success)
                        {
                            return Ok("SUCCESS - API Key: " + serviceResponse.ResponseObject);
                        }
                        else
                        {
                            return Ok("FAILURE - There was a problem generating the API Key.");
                        }
                    }
                    else
                    {
                        return Ok("FAILURE - Incorrect Admin Password.");
                    }

                }
                else
                {
                    return Ok("FAILURE - You must provide teh Admin Password.");
                }
            }
            catch (Exception ex)
            {
                AdminLogService.PostItem(new AdminLogMessage() { Title = "ERROR", Message = ex.Message });
                return BadRequest(ex);
            }
        }

    }
}