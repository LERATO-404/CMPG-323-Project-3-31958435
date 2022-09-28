using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDevicesRepository
    {
        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        { }

        //Returns all devices with the zone and category the each device is in
        public IEnumerable<Device> GetAlldevice()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return  connectedOfficeContext.ToList();
        }

        //Return a single device with the category and zone the device is in
        public Device GetDeviceById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var device = _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefault(m => m.DeviceId == id);
            if (device == null)
            {
                return null;
            }
            return device;
        }

        // Return the Category Model to list all the categories
        public DbSet<Category> GetCategory()
        {
            return _context.Category;
        }

        // Return the Zone Model to list all the Zones
        public DbSet<Zone> GetZone()
        {
            return _context.Zone;
        }


    }
}
