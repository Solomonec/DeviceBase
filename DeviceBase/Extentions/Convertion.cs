using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceBase.BLL.Implement;
using DeviceBase.Models;
using DeviceBase.ViewModels;

namespace DeviceBase.Extentions
{
    public static class Convertion
    {

       // public string ContentType { get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; } }

        public static DataTable ListToDataTable<T>(this List<T> data)
        {
            DataTable datatable = new DataTable();
            if (data == null)
            {
                return null;
            }

            PropertyDescriptorCollection propcol = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in propcol)
            {
                datatable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            object[] datavalues = new object[propcol.Count];
            foreach (T item in data)
            {

                for (int i = 0; i < datavalues.Length; i++)
                {
                    datavalues[i] = propcol[i].GetValue(item);
                }

                datatable.Rows.Add(datavalues);
            }

            return datatable;
        }

        public static IEnumerable<SelectListItem> EntitiesToSelectList<T>(IEnumerable<T> data)
        {
            return data
                .Select(x => new SelectListItem
                {
                    Value = "",
                    Text = x.ToString()
                });
         
        }


        public static ManageModel ToManageModel(this UserProfile profile)
        {
            ManageModel resultmodel = new ManageModel
            {
                UserId = profile.UserId,
                UserName = profile.UserName,
                FullName = profile.FullName,
                Job = profile.Job,
                Slugba = profile.Slugba,
                Department = profile.Department,
                Tel = profile.Tel,
                Active = profile.Active
            };
           

            return resultmodel;
        }

        public static UserProfile ToUserProfile(this ManageModel manage)
        {
            UserProfile resultmodel = new UserProfile
            {
                UserId = manage.UserId,
                UserName = manage.UserName,
                FullName = manage.FullName,
                Job = manage.Job,
                Slugba = manage.Slugba,
                Department = manage.Department,
                Tel = manage.Tel,
                Active = manage.Active
            };


            return resultmodel;
        }

        public static IQueryable<FilterResultModel> ToFilterResultModel(this IQueryable<ItDevice> device)
        {
            List<FilterResultModel> resultmodel = new List<FilterResultModel>();

            foreach (var item in device)
            {
                FilterResultModel resultitem = new FilterResultModel
                {
                    DevInvNum = item.DevInvNum,
                    DevBuhInvNumber = item.DevBuhInvNumber,
                    DeviceClass = item.DeviceClass,
                    DeviceType = item.DeviceType,
                    DeviceSubType = "",
                    DateInWork = item.DateInWork,
                    SerialNumber = item.SerialNumber,
                    DeviceModel = item.DeviceModel,
                    OwnerName = item.DeviceOwner.FullName,
                    Job = item.DeviceOwner.Job,
                    Slugba = item.DeviceOwner.Slugba,
                    Department = item.DeviceOwner.Department,
                    Address = item.DeviceOwner.Address,
                    Room = item.DeviceOwner.Room,
                    Floor = item.DeviceOwner.Floor,
                    Tel = item.DeviceOwner.Tel,
                    Location = item.Location,
                    Comment = item.Comment
                };

                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<FilterResultModel> ToFilterResultModel(this IQueryable<AsppDevice> device)
        {
            List<FilterResultModel> resultmodel = new List<FilterResultModel>();

            foreach (var item in device)
            {
                FilterResultModel resultitem = new FilterResultModel
                {
                    DevInvNum = item.DevInvNum,
                    DevBuhInvNumber = item.DevBuhInvNumber,
                    DeviceClass = item.DeviceClass,
                    DeviceType = item.DeviceType,
                    DeviceSubType = "",
                    DateInWork = item.DateInWork,
                    SerialNumber = item.SerialNumber,
                    DeviceModel = item.DeviceModel,
                    OwnerName = item.DeviceOwner.FullName,
                    Job = item.DeviceOwner.Job,
                    Slugba = item.DeviceOwner.Slugba,
                    Department = item.DeviceOwner.Department,
                    Address = item.DeviceOwner.Address,
                    Room = item.DeviceOwner.Room,
                    Floor = item.DeviceOwner.Floor,
                    Tel = item.DeviceOwner.Tel,
                    Location = item.Location,
                    Comment = item.Comment
                };

                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<FilterResultModel> ToFilterResultModel(this IQueryable<PaDevice> device)
        {
            List<FilterResultModel> resultmodel = new List<FilterResultModel>();

            foreach (var item in device)
            {
                FilterResultModel resultitem = new FilterResultModel
                {
                    DevInvNum = item.DevInvNum,
                    DevBuhInvNumber = item.DevBuhInvNumber,
                    DeviceClass = item.DeviceClass,
                    DeviceType = item.DeviceType,
                    DeviceSubType = item.DeviceSubType,
                    DateInWork = item.DateInWork,
                    SerialNumber = item.SerialNumber,
                    DeviceModel = item.DeviceModel,
                    OwnerName = item.DeviceOwner.FullName,
                    Job = item.DeviceOwner.Job,
                    Slugba = item.DeviceOwner.Slugba,
                    Department = item.DeviceOwner.Department,
                    Address = item.DeviceOwner.Address,
                    Room = item.DeviceOwner.Room,
                    Floor = item.DeviceOwner.Floor,
                    Tel = item.DeviceOwner.Tel,
                    Location = item.Location,
                    Comment = item.Comment
                };

                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static FilterEntry GetEntry(string deviceclass)
        {
            switch (deviceclass)
            {
               case "Офисное оборудование" :return FilterEntry.It;
               case "Сетевое оборудование": return FilterEntry.It;
               case "Оборудование АСПП": return FilterEntry.Aspp;
               case "Оборудование ПА": return FilterEntry.Pa;
               default:return FilterEntry.It;
            }
        }

    }
}