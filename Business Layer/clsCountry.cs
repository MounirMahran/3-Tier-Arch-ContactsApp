using ContactsApp_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolApp_BusinessLayer
{
    public class clsCountry
    {
        private enum enMode { Update, AddNew }
        public int ID { get; set; }
        public string Name { get; set; }

        public int Code { get; set; }

        public int PhoneCode { get; set; }
        private enMode Mode { get; set; }
        public clsCountry()
        {
            ID = -1;
            Name = "";
            Mode = enMode.AddNew;
            Code = -1;
            PhoneCode = -1;
        }

        public clsCountry(int id, string name, int code, int phonecode)
        {
            ID = id;
            Name = name;
            Code = code;
            PhoneCode = phonecode;
            Mode = enMode.Update;
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            int Code = -1;
            int PhoneCode = -1;


            if (clsDataAccessLayer.GetCountryByID(ID, ref CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }

        }

        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            int Code = -1;
            int PhoneCode = -1;

            if (clsDataAccessLayer.GetCountryByName(ref ID, CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }

        }
        private bool _AddNewCountry()
        {
            return clsDataAccessLayer.AddNewCountry(this.Name, this.Code, this.PhoneCode) != -1;
        }

        private bool _UpdateCountry()
        {
            return clsDataAccessLayer.UpdateCountry(this.ID, this.Name, this.Code, this.PhoneCode) > 0;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    return false;


                case enMode.Update:
                    if (_UpdateCountry())
                    {
                        return true;
                    }
                    return false;



            }
            return false;
        }

        public static bool DeleteCountry(int ID)
        {
            return clsDataAccessLayer.DeleteCountry(ID) > 0;
        }

        public static DataTable GetAllCountries()
        {
            return clsDataAccessLayer.GetAllCountries();
        }

        public static bool IsExist(int ID)
        {
            return clsDataAccessLayer.IsCountryExist(ID);
        }

        public static bool IsExist(String Name)
        {
            return clsDataAccessLayer.IsCountryExist(Name);
        }

    }
}
