using DeviceManagement_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Interfaces
{
    public interface IUnitOfWork
    {

        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Zone> ZoneRepository { get; }
        IGenericRepository<Device> DeviceRepository { get; }

        IDevicesRepository DeviceRepo { get; }

        void Save();
    }
}
