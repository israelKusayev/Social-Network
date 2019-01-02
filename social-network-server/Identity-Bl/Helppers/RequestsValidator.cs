using Identity_Common.Interfaces.Helppers;
using Identity_Common.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Identity_Bl.Helppers
{
    public class RequestsValidator : IRquestsValidator
    {
        public string ValidateUser(User user,string tokenId)
        {
            List<string> errors = new List<string>();
            if (user.Age < 0)
                errors.Add("age most be positive integer");
            if (user.Email != null && !new EmailAddressAttribute().IsValid(user.Email))
                errors.Add("invalid email address");
            if(user.UserId!= tokenId)
            {
                errors.Add("user missing requiered previliges");
            }
            return errors.Count == 0 ? null : String.Join(",", errors);
        }


    }
}
