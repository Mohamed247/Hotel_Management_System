﻿using Backend.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Routing;

namespace Backend.Controllers
{

    public class WorkerController : ApiController
    {
        private static ExpandoObjectConverter converter = new ExpandoObjectConverter();
        [HttpPost]
        public dynamic add([FromBody] dynamic obj) //api/worker/add
        {
            AbstractWorker worker;
            if (obj.jobTitle == JobTitle.RoomService)
            {
                worker = new RoomService(Convert.ToString(obj.userName), Convert.ToInt32(obj.age), Convert.ToString(obj.email), Convert.ToString(obj.phoneNumber), Convert.ToInt32(obj.salary), Convert.ToString(obj.jobTitle), Convert.ToString(obj.incomeType));
            }
            else if (obj.jobTitle == JobTitle.Receptionist)
            {
                worker = new Receptionist(Convert.ToString(obj.userName), Convert.ToInt32(obj.age), Convert.ToString(obj.email), Convert.ToString(obj.phoneNumber), Convert.ToInt32(obj.salary), Convert.ToString(obj.jobTitle), Convert.ToString(obj.incomeType), Convert.ToString(obj.password));
            }
            else
            {
                worker = new Manager(Convert.ToString(obj.userName), Convert.ToInt32(obj.age), Convert.ToString(obj.email), Convert.ToString(obj.phoneNumber), Convert.ToInt32(obj.salary), Convert.ToString( obj.jobTitle), Convert.ToString(obj.incomeType), Convert.ToString(obj.password));
            }
            Manager.addWorker(worker, Convert.ToString(obj.password));
            dynamic resp = new ExpandoObject();
            resp.Success = true;
            return resp;
        }

        [HttpPost]
        public dynamic edit([FromBody] dynamic obj) //api/worker/edit
        {
            AbstractWorker worker;
            if (Convert.ToString(obj.jobTitle) == JobTitle.RoomService)
            {
                worker = new RoomService(Convert.ToString(obj.userName), Convert.ToInt32(obj.age), Convert.ToString(obj.email), Convert.ToString(obj.phoneNumber), Convert.ToInt32(obj.salary), Convert.ToString(obj.jobTitle), Convert.ToString(obj.incomeType));
            }
            else if(Convert.ToString(obj.jobTitle) == JobTitle.Receptionist)
            {
                worker = new Receptionist(Convert.ToString(obj.userName), Convert.ToInt32(obj.age), Convert.ToString(obj.email), Convert.ToString(obj.phoneNumber), Convert.ToInt32(obj.salary), Convert.ToString(obj.jobTitle), Convert.ToString(obj.incomeType), Convert.ToString(obj.password));
            }
            else
            {
                worker = new Manager(Convert.ToString(obj.userName), Convert.ToInt32(obj.age), Convert.ToString(obj.email), Convert.ToString(obj.phoneNumber), Convert.ToInt32(obj.salary), Convert.ToString(obj.jobTitle), Convert.ToString(obj.incomeType), Convert.ToString(obj.password));
            }
            Manager.editWorker(worker, Convert.ToString(obj.password));
            dynamic resp = new ExpandoObject();
            resp.Success = true;
            return resp;
        }

        [HttpPost]
        public dynamic delete([FromBody] dynamic obj) //api/worker/delete
        {
            Manager.deleteWorker(Convert.ToInt32(obj.id));
            dynamic resp = new ExpandoObject();
            resp.Success = true;

            return resp;
        }

        [HttpPost]
        public dynamic get([FromBody] dynamic obj) //api/worker/get
        {
            string json = JsonConvert.SerializeObject(Manager.getWorker(Convert.ToInt32(obj.id)));
            dynamic resp = JsonConvert.DeserializeObject(json);
            return resp;
        }

        [HttpPost]
        public dynamic getAll() //api/worker/getAll
        {
            dynamic resp = new ExpandoObject();
            resp.lst = new List<object>(Manager.viewAllWorkers());
            return resp;
        }

    }
}