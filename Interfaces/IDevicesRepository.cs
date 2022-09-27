using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement_WebApp.Interfaces
{
    public interface IDevicesRepository : IGenericRepository<Device>
    {
        //GET: All all Devices 
        IEnumerable<Device> GetAlldevice();

        //GET a specific device
        Device GetDeviceById(Guid? id);

        DbSet<Category> GetCategory();
        DbSet<Zone> GetZone();

        


    }
}
