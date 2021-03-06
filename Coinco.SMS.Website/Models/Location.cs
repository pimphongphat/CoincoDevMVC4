﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using Coinco.SMS.AXWrapper;
using StructureMap;

namespace Coinco.SMS.Website.Models
{
    public class Location
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationQty { get; set; }
        public string LocationandQty { get; set; }

        public SelectList LocationList { get; set; } 

        public Location()
        {

        }

        public Location(string locationId, string locationName, string locationQty, string locationandQty)
        {
            this.LocationId = locationId;
            this.LocationName = locationName;
            this.LocationQty = locationQty;
            this.LocationandQty = locationandQty;
             
        }

        public List<Location> GetLocations(string itemNumber, string site, string warehouse, string userName)
        {
            IAXHelper axHelper = ObjectFactory.GetInstance<IAXHelper>();
            List<Location> locationList = new List<Location>();

            try
            {
                DataTable resultTable = axHelper.GetLocations(itemNumber, site, warehouse, userName);

                foreach (DataRow row in resultTable.Rows)
                {
                    Location locationObject = new Location();
                    locationObject.LocationId = row["LocationID"].ToString();
                    locationObject.LocationQty = row["PhysicalQty"].ToString();
                    locationObject.LocationandQty = locationObject.LocationId  + " [Qty - " + locationObject.LocationQty + " ]"; 
                    locationList.Add(locationObject);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return locationList;

        }
    }
}