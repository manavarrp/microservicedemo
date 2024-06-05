﻿using Ordering.Domain.Common;

namespace Ordering.Domain.Models
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Decimal TotalPrice { get; set; }

        //Billing Address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        //Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string Expiration {  get; set; }
        public int PaymentMethod { get; set; }
    }
}