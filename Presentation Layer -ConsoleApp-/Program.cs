using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ConsolApp_BusinessLayer;

namespace ContactsConsolApp_PresentationLayer
{
    internal class Program
    {
        static void testFindContact(int ContactID)
        {
            clsContact newContact = clsContact.Find(ContactID);

            if (newContact != null)
            {
                Console.WriteLine("Id         : " + newContact.ContactID);
                Console.WriteLine("First Name : " + newContact.FirstName);
                Console.WriteLine("Last Name  : " + newContact.LastName);
                Console.WriteLine("Email      : " + newContact.Email);
                Console.WriteLine("Phone      : " + newContact.Phone);
                Console.WriteLine("Address    : " + newContact.Address);
                Console.WriteLine("Country Id : " + newContact.CountryID);
                Console.WriteLine("Img Path   : " + newContact.ImgPath);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Contact with ID [ " + ContactID + " ] is not found");
            }
        }

        static void testAddNewContact()
        {
            clsContact NewContact = new clsContact();

            NewContact.FirstName = "Moataz";
            NewContact.LastName = "Mahran";
            NewContact.Email = "rew@fsd.com";
            NewContact.Phone = "1234567890";
            NewContact.Address = "Qena";
            NewContact.DateOfBirth = DateTime.Now;
            NewContact.CountryID = 1;
            NewContact.ImgPath = "";

            if (NewContact.Save())
            {
                Console.WriteLine("Contact Added Successfully with ID: " + NewContact.ContactID);
            }
            else
            {
                Console.WriteLine("Contact Not Added Successfully");
            }
        }

        static void testUpdateContact(int ContactID)
        {
            clsContact NewContact = clsContact.Find(ContactID);

            if (NewContact != null)
            {
                NewContact.FirstName = "Samir";
                NewContact.LastName = "Kamona";
                NewContact.Email = "rew@fsd.com";
                NewContact.Phone = "1234567890";
                NewContact.Address = "Qena";
                NewContact.DateOfBirth = DateTime.Now;
                NewContact.CountryID = 1;
                NewContact.ImgPath = "";


                if (NewContact.Save())
                {
                    Console.WriteLine("Contact Updated Successfully");
                }
                else
                {
                    Console.WriteLine("Failed to update Contact");
                }

            }
            else
            {
                Console.WriteLine("Contact can not be found");
            }
        }

        static void testDeleteContact(int ContactID)
        {
            if (clsContact.DeleteContact(ContactID))
            {
                Console.WriteLine("Contact Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Failed to Delete Contact");
            }
        }

        /*static void testListContacts()
        {
            List<clsContact> Contacts = clsContact.ListContacts();

            foreach (clsContact Contact in Contacts)
            {
                Console.WriteLine("Id         : " + Contact.ContactID);
                Console.WriteLine("First Name : " + Contact.FirstName);
                Console.WriteLine("Last Name  : " + Contact.LastName);
                Console.WriteLine("Email      : " + Contact.Email);
                Console.WriteLine("Phone      : " + Contact.Phone);
                Console.WriteLine("Address    : " + Contact.Address);
                Console.WriteLine("DateofBirth: " + Contact.DateOfBirth);
                Console.WriteLine("Country Id : " + Contact.CountryID);
                Console.WriteLine("Image Path : " + Contact.ImgPath);
            }
        }
        */

        static void testGetAllContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}, {row["FirstName"]}, {row["LastName"]}");
            }
        }
        static void testIsExist(int ID)
        {
            if (clsContact.IsExist(ID))
            {
                Console.WriteLine("Contact is found");
            }
            else
            {
                Console.WriteLine("Contact is not found");
            }
        }

        static void testFindCountry(int CountryID)
        {
            clsCountry newCountry = clsCountry.Find(CountryID);
            if (newCountry != null) {
                Console.WriteLine("Country ID       : " + newCountry.ID);
                Console.WriteLine("Country Name     : " + newCountry.Name);
                Console.WriteLine("Country Code     : " + newCountry.Code);
                Console.WriteLine("Country PhoneCode: " + newCountry.PhoneCode);
            }
            else
            {
                Console.WriteLine("Country Can not be found");
            }
        }

        static void testFindCountry(string CountryName)
        {
            clsCountry newCountry = clsCountry.Find(CountryName);
            if (newCountry != null)
            {
                Console.WriteLine("Country ID       : " + newCountry.ID);
                Console.WriteLine("Country Name     : " + newCountry.Name);
                Console.WriteLine("Country Code     : " + newCountry.Code);
                Console.WriteLine("Country PhoneCode: " + newCountry.PhoneCode);
            }
            else
            {
                Console.WriteLine("Country Can not be found");
            }
        }

        static void testAddNewCountry()
        {
            clsCountry newCountry = new clsCountry();

            newCountry.Name = "Qena";
            newCountry.Code = 12;
            newCountry.PhoneCode = 14;

            if (newCountry.Save())
            {
                Console.WriteLine("Country Added Successfully");
            }
            else
            {
                Console.WriteLine("Failed to Addd Country");
            }
        }

        static void testUpdateCountry()
        {
            clsCountry Country = clsCountry.Find(8);

            if (Country != null)
            {

                Country.Name = "Wales";
                Country.Code = 134;
                Country.PhoneCode = 212;

                if (Country.Save())
                {
                    Console.WriteLine("Country Updated Successfully");
                }
                else
                {
                    Console.WriteLine("Failed to Update Country");
                }
            }
            else
            {
                Console.WriteLine("Country Can not be found");
            }
        }

        static void testDeleteCountry(int ID)
        {
            if (clsCountry.DeleteCountry(ID))
            {
                Console.WriteLine("Country Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete Country");
            }
        }

        static void testGetAllCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();

           
                foreach(DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["CountryID"]} {row["CountryName"]} {row["Code"]} {row["PhoneCode"]}");
                    Console.WriteLine();
                }
           
        }

        static void testIsCountryExist(int ID)
        {
            if (clsCountry.IsExist(ID))
            {
                Console.WriteLine("Country Exists");
            }
            else
            {
                Console.WriteLine("Country does not exist");
            }
        }

        static void testIsCountryExist(string Name)
        {
            if (clsCountry.IsExist(Name))
            {
                Console.WriteLine("Country Exists");
            }
            else
            {
                Console.WriteLine("Country does not exist");
            }
        }
        static void Main(string[] args)
        {
            //testFindContact(2);
            //testAddNewContact();
            //testUpdateContact(233);
            //testDeleteContact(22);
            //testGetAllContacts();
            //testIsExist(1);

            //testFindCountry(8);
            //testFindCountry("Wales");
            //testAddNewCountry();
            //testFindCountry(9);
            //Console.WriteLine();
            //testUpdateCountry();
            //Console.WriteLine();
            //testFindCountry(8);
            //testDeleteCountry(7);
            testGetAllCountries();
            //testIsCountryExist(5);
            //testIsCountryExist("Wales");


            Console.ReadLine();

        }
    }
}
