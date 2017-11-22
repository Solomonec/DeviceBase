using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;

namespace DeviceBase.BLL.Interfaces
{
    public interface IFactoryRepository
    {

        IDeviceRepository<ItDevice> ItDeviceRepositoryCreate();

        IDeviceRepository<AsppDevice> AsppDeviceRepositoryCreate();

        IDeviceRepository<PaDevice> PaDeviceRepositoryCreate();

        IOwnerRepository OwnerRepositoryCreate();

        IDeviceClassRepository DeviceClassRepositoryCreate();

        ILocationRepository LocationRepositoryCreate();

        IDeviceTypeRepository DeviceTypeRepositoryCreate();

        IUserRepository UserRepositoryCreate();
        
        IDataExport DataExportCreate();

        IDataFilter DataFilterCreate();
        
        IStatistics StatisticsCreate();


    }
}
