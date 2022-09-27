using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Interfaces;
using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConnectedOfficeContext _context;
        public IGenericRepository<Category> categoryRepository;
        public IGenericRepository<Zone> zoneRepository;
        public IGenericRepository<Device> deviceRepository;
        public IDevicesRepository deviceRepo;
        

        public UnitOfWork(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                if(this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(_context);
                }
                return categoryRepository;
            }
        }
        public IGenericRepository<Zone> ZoneRepository {
            get
            {
                if (this.zoneRepository == null)
                {
                    this.zoneRepository = new GenericRepository<Zone>(_context);
                }
                return zoneRepository;
            }
        }
        public IGenericRepository<Device> DeviceRepository
        {
            get
            {
                if (this.deviceRepository == null)
                {
                    this.deviceRepository = new GenericRepository<Device>(_context);
                }
                return deviceRepository;
            }
        }

        public IDevicesRepository DeviceRepo {
            get
            {
                if (this.deviceRepo == null)
                {
                    this.deviceRepo = new DeviceRepository(_context);
                }
                return deviceRepo;
            }
        } 

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
