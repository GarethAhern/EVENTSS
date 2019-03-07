using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndInvitesLogic.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("First Name")]
        public string FirstName { get; set; }

        [System.ComponentModel.DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Formal Salutation is Dear Mr Smith
        /// Informal Salutation is Hello John
        /// </summary>
        public bool FormalSalutation { get; set; }
        public string BusinessName { get; set; }
        public string EmailAddress { get; set; }
        public AddressModel WorkAddress { get; set; }
        public AddressModel HomeAddress { get; set; }
        public string CellPhone { get; set; }
        public string WorkPhone { get; set; }

        /// <summary>
        /// Free field to enter dietry notes
        /// </summary>
        public string DietaryRequirementsNotes { get; set; }


        /// <summary>
        ///This will hold a list of common dietry requirements
        ///No point making this a class because it may change
        ///So this will get populated from a table in SQL
        ///If they have something in this list then they are that e.g Vegan, Vegetarian
        /// </summary>
        public List<string> DietryList { get; set; }

        public List<string> Interests { get; set; }
        public String PartnerFullName { get; set; }

        /// <summary>
        /// Identifies the importance of the client e.g Platinum, Gold, Silver, Bronze
        /// </summary>
        public string Category { get; set; }

        public string ClientBasicDisplayInfo
        {
            get
            {
                return $"{Title} { FirstName.Substring(0,1) } { LastName } - { BusinessName }";
            }
        }

        public ClientModel()
        {

        }   
        
        public ClientModel(string firstName,string lastName,string title, string formalSalutation,string businessName, string emailAddress, string cellPhone, string workPhone)
        {
            FirstName = firstName;
            LastName = lastName;
            Title = title;

            bool formalSalutationValue = true;
            bool.TryParse(formalSalutation, out formalSalutationValue);
            FormalSalutation = formalSalutationValue;

            BusinessName = businessName;
            EmailAddress = emailAddress;
            CellPhone = cellPhone;
            WorkPhone = workPhone;       
        }    
    }
    }
