using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ContactsApp_DataAccessLayer;

namespace ConsolApp_BusinessLayer
{
    public class clsContact
    {
        public enum enMode { Update, AddNew}
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CountryID { get; set; }

        public string ImgPath { get; set; }

        private enMode Mode { get; set; }

        public clsContact()
        {
            this.ContactID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = 1;
            this.ImgPath = "";

            this.Mode = enMode.AddNew;
        }
        private clsContact(int contactID, string firstName, string lastName, string email, string phone, string address, DateTime dateOfBirth, int countryID, string imgPath)
        {
            ContactID = contactID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            CountryID = countryID;
            ImgPath = imgPath;

            Mode = enMode.Update;
        }

        public static clsContact Find(int contactID)
        {
            string FirstName = "",  LastName = "",  Email = "",  Phone = "",  Address = "",  ImgPath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = 0;


            if(clsDataAccessLayer.GetContactInfoByID(contactID, ref FirstName, ref LastName, ref Email,
                                                                ref Phone, ref Address, ref DateOfBirth,
                                                                ref CountryID, ref ImgPath))
            {
                return new clsContact(contactID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImgPath);
            }

            else
            {
                return null;
            }
        }

        private bool _AddNewContact()
        {
            this.ContactID = clsDataAccessLayer.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address,this.DateOfBirth, this.CountryID, this.ImgPath);
            return (ContactID != -1);
            
        }

        private bool _UpdateContact()
        {
            return clsDataAccessLayer.UpdateContact(this.ContactID, this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImgPath);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        this.Mode = enMode.Update;
                        return true;

                    }
                    else 
                    { 
                        return false; 
                    }

                case enMode.Update:
                    if(_UpdateContact())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default : return false;
            }
        }
        public static bool DeleteContact(int ContactID)
        {
            return clsDataAccessLayer.DeleteContact(ContactID) > 0;

        }

        /*public static List<clsContact> ListContacts()
        {
            List<clsContact> contacts = new List<clsContact>();
            clsContact newContact = new clsContact();

            //Retrieve Contacts from the data access layer
            List<dynamic> dynamicContacts = clsDataAccessLayer.ListContacts();
            
            //Convert dynamic contact to clscontact type

            foreach(dynamic contact in dynamicContacts)
            {
                newContact.ContactID = contact.ContactID;
                newContact.FirstName = contact.FirstName;
                newContact.LastName = contact.LastName;
                newContact.Email = contact.Email;
                newContact.Phone = contact.Phone;
                newContact.Address = contact.Address;
                newContact.DateOfBirth = contact.DateOfBirth;
                newContact.ImgPath = contact.ImagePath;


                contacts.Add(newContact);

            }
            
            

            return contacts;

        }*/
        public static DataTable GetAllContacts()
        {
            return clsDataAccessLayer.GetAllContacts();
        }

        public static bool IsExist(int ContactID)
        {
            return clsDataAccessLayer.IsContactExist(ContactID);
        }
    
    
    }

    
}
