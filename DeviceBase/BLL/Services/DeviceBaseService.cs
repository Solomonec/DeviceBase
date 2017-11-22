using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;

namespace DeviceBase.BLL.Services
{
    public class DeviceBaseService
    {
        private readonly IFactoryRepository _repository;

        public DeviceBaseService(IFactoryRepository repository)
        {
            _repository = repository;
        }

        public IDeviceRepository<ItDevice> ItDeviceRepository
        {
            get { return _repository.ItDeviceRepositoryCreate(); }
        }


        public IDeviceRepository<AsppDevice> AsppDeviceRepository
        {
            get { return _repository.AsppDeviceRepositoryCreate(); }
        }


        public IDeviceRepository<PaDevice> PaDeviceRepository
        {
            get { return _repository.PaDeviceRepositoryCreate(); }
        }

        public IOwnerRepository OwnerRepository
        {
            get { return _repository.OwnerRepositoryCreate(); }
        }

        public IDeviceTypeRepository DeviceTypeRepository
        {
            get { return _repository.DeviceTypeRepositoryCreate(); }
        }

        public IDeviceClassRepository DeviceClassRepository
        {
            get { return _repository.DeviceClassRepositoryCreate(); }
        }

        public ILocationRepository LocationRepository
        {
            get { return _repository.LocationRepositoryCreate(); }
        }

        public IUserRepository UserRepository
        {
            get { return _repository.UserRepositoryCreate(); }
        }

        public IDataExport DataExport
        {
            get { return _repository.DataExportCreate(); }
        }
        public IDataFilter DataFilter
        {
            get { return _repository.DataFilterCreate(); }
        }

        public IStatistics Statistics
        {
            get { return _repository.StatisticsCreate(); }
        }

    }
}
