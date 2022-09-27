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

        //Retrieve all items
        public IEnumerable<Device> GetAlldevice()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return  connectedOfficeContext.ToList();
        }

        //Retrieve one specified item
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

        public DbSet<Category> GetCategory()
        {
            return _context.Category;
        }

        public DbSet<Zone> GetZone()
        {
            return _context.Zone;
        }


    }
}
